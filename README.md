# Prodtest demo

This is the readme for the prodtest demo project. For more information, check the [docs](https://brdv.github.io/prodtest-docs).

## Contents

The contents of this Readme are:

- [Prerequisites](#prerequisites)
- [Setup](#setup)
- [Other commands](#other-commands)

## Prerequisites

In order to run the project, please make sure the following tools are installed:

- [Docker](https://docs.docker.com/get-docker/)
- [Kubectl](https://kubernetes.io/docs/tasks/tools/)
- [Helm](https://helm.sh/docs/intro/install/)

\* **Note:** Please use [WSL](https://learn.microsoft.com/en-us/windows/wsl/install) if you are on windows

To check if everyting is installed correctly, run the following commands:

```bash
# for docker
docker --version
# for kubectl
kubectl version --short
# for helm
helm version --short
```

If it outputs a version number, the installation has succeeded.

## Setup

Follow the guide below to setup and start the project.

1.  Once all prerquisites are installed, make sure you have a docker kubernetes cluster running following [this guide](https://docs.docker.com/desktop/kubernetes/#enable-kubernetes).

2.  Next make sure the docker-desktop kubernetes context is selected for kubectl:

    ```bash
    # 1) get all contexts
    kubectl config get-contexts

    # 2) set docker-desktop context if not set allready *
    kubectl config set-context docker-desktop
    ```

    \* you can see if docker-desktop is selecten in the output of command 1.

3.  Last setup step, apply all resources:

    ```bash
    # from the projects root folder:
    sh ./scripts/kubernetes/setup-darklaunch.sh
    ```

    This will apply all resources to the cluster. You will see all resources in the console. (Run `kubectl get all` if they do not show up.)

4.  Go to http://localhost/api/health, you should see a json object as follows:

    ```json
    {
      "health": "ok"
    }
    ```

5.  To see the traefik dashboard, run the following command:

    ```bash
    kubectl port-forward $(kubectl get pods --selector "app.kubernetes.io/name=traefik" --output=name) 9000:9000
    ```

    Leave the shell open and go to http://localhost:9000/dashboard/

## Other commands

There are a few other commands that can be helpfull during testing or development.

1. Cleanup kubernetes and helm char

   If for any reason you want to clean up the configured kubernetes resources you can enter the following command in the terminal:

   ```bash
   sh ./scripts/kubernetes/cleanup.sh
   ```

2. Deploy new docker images

   If you want to deploy new versions of the docker images you can run any of the scripts in `./scripts/docker`
   For example to deploy a new version of the latest image:

   ```bash
   sh ./scripts/docker/latest.deploy.sh
   ```
