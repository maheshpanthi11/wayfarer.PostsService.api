# Wayfarer Posts Service - Documentation

## Requirement Understanding

Multi-tier Kubernetes deployment with:

- One service/API tier exposed outside the cluster.
- One database tier accessible only inside the cluster.
- Docker images built and pushed to Docker Hub.
- Kubernetes-native configuration using ConfigMaps and Secrets.
- Persistent database storage that survives pod recreation.
- Rolling updates for the API tier.
- Self-healing behavior for both tiers.
- Horizontal Pod Autoscaling for the API tier.
- FinOps awareness through resource requests, limits, and optimization opportunities.

The implementation in this repository follows that model with a .NET API service and a PostgreSQL database.

## Assumptions

- The API tier is the primary application surface for the assignment and should be the externally accessible entry point.
- The database only needs to serve the application and should not be exposed publicly.
- Seeded database data is acceptable for demonstrating persistence and retrieval.
- Kubernetes Ingress is the preferred external access method for the final deployment.
- GKE is the target cloud environment, with Minikube used for local validation when needed.

## Solution Overview

### Application Stack

- API: .NET 10 service built from `src/wayfarer.PostsService.api`
- Database: PostgreSQL 15 running as a StatefulSet
- Container image: pushed to Docker Hub
- Orchestration: Kubernetes manifests in `deployment/`
- CI/CD: GitHub Actions workflow in `.github/workflows/deploy.yml`

### Architecture

- External traffic enters through Kubernetes Ingress.
- Ingress routes to the API Service.
- The API Deployment runs 4 replicas by default.
- The API connects to PostgreSQL using the internal Kubernetes Service name, not a pod IP.
- PostgreSQL runs as a StatefulSet with one replica and a persistent volume claim.

### Kubernetes Resources

- `ConfigMap` for non-sensitive application and database settings.
- `Secret` for the database password.
- `Deployment` for the API tier with rolling update strategy.
- `Service` for the API tier.
- `Ingress` for external access.
- `HorizontalPodAutoscaler` for API scaling.
- `StatefulSet` for PostgreSQL persistence and recovery.
- `Headless Service` for stable database identity.

### Data Model

The database contains a `Post` table with seeded records. The table is used to demonstrate read operations from the API and to prove that data remains available after database pod recreation.

## Justification for the Resources Utilized

### API Tier

- `replicas: 4`: satisfies the assignment requirement for the service tier and gives enough baseline capacity for HPA to scale from.
- `RollingUpdate` with `maxSurge: 1` and `maxUnavailable: 0`: supports zero-downtime releases.
- CPU requests/limits: `100m` request and `500m` limit.
- Memory requests/limits: `256Mi` request and `512Mi` limit.

These values are intentionally moderate. They keep the API schedulable on small clusters while still leaving room for burst traffic.

### Database Tier

- `replicas: 1`: A single database instance.
- Persistent storage: `5Gi` PVC.
- Storage class: `standard`.
- CPU requests/limits: `100m` request and `500m` limit.
- Memory requests/limits: `256Mi` request and `512Mi` limit.

These values are sufficient for a small seeded dataset and keep the database lightweight.

### HPA

- Minimum replicas: `4`
- Maximum replicas: `10`
- CPU target: `70%`
- Memory target: `80%`
- Scale-down stabilization: `30s`

This setup supports visible scaling high load while avoiding excessive replica thrashing.

## Configuration and Security

- Database host, port, database name, and user are stored in `wayfarer-posts-config`.
- The database password is stored in `postgres-secret`.
- The API reads its connection settings from environment variables populated by Kubernetes.
- The API and database use Kubernetes Services for communication, not pod IPs.

## Deployment Strategy

### Rolling Updates

The API Deployment uses Kubernetes rolling updates to replace pods gradually. This keeps the service available during image updates and is suitable for demonstrating deployment strategy in the recording.

### Self-Healing

- The API Deployment will recreate failed pods automatically.
- PostgreSQL as a StatefulSet will recreate its pod while preserving attached storage.
- Readiness and liveness probes help Kubernetes detect unhealthy containers.

### Persistence

The PostgreSQL pod writes to a persistent volume claim mounted at `/var/lib/postgresql/data`. Recreating the pod does not remove the volume, so the data remains intact.

## FinOps Considerations

### Cost Optimization Opportunities

1. Right-size CPU and memory requests after observing actual usage.
2. Keep the database on a single replica because it is stateful and low-volume set up.
3. Use HPA to scale only the API tier under demand instead of overprovisioning all the time.
4. Use a smaller node pool or cluster autoscaling in a real GKE environment.
5. Reduce PVC size if the dataset stays small.
6. Monitor resource usage and to find the opportunity to reduce the cost.
7. Observed metrics from `kubectl top pods` and HPA usage should guide tuning. They can be reduced if the live workload shows lower consumption.

## Submission Notes

- Repository URL: `https://github.com/maheshpanthi11/wayfarer.PostsService.api`
- Docker Hub image: `https://hub.docker.com/r/maheshpanthi079/wayfarer-posts-service`
- External API URL: provided by the Ingress after deployment

