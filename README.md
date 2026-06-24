# Wayfarer Posts Service
Post service repository of image sharing social platform

## Repository Links
- **GitHub Repository**: [wayfarer.PostsService.api](https://github.com/maheshpanthi11/wayfarer.PostsService.api)
- **Docker Hub Image**: [maheshpanthi079/wayfarer-posts-service](https://hub.docker.com/r/maheshpanthi079/wayfarer-posts-service)

## Service API External URL
The external URL is created by the Kubernetes Ingress and is not fixed in advance. After deployment, run:

```bash
kubectl get ingress -n wayfarer
```

That prints the exact public IP ADDRESS, such as `8.233.86.95`.

## Comprehensive Document ##

More information here: [Documentation](./Documentation.md)
