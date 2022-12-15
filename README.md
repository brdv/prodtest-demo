[![SonarCloud](https://sonarcloud.io/images/project_badges/sonarcloud-black.svg)](https://sonarcloud.io/summary/new_code?id=prodtest-demo)

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=prodtest-demo&metric=alert_status&token=9005461108c59dec16be6c57760d5aaaea5d6564)](https://sonarcloud.io/summary/new_code?id=prodtest-demo)

[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=prodtest-demo&metric=coverage&token=9005461108c59dec16be6c57760d5aaaea5d6564)](https://sonarcloud.io/summary/new_code?id=prodtest-demo) [![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=prodtest-demo&metric=sqale_rating&token=9005461108c59dec16be6c57760d5aaaea5d6564)](https://sonarcloud.io/summary/new_code?id=prodtest-demo) [![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=prodtest-demo&metric=vulnerabilities&token=9005461108c59dec16be6c57760d5aaaea5d6564)](https://sonarcloud.io/summary/new_code?id=prodtest-demo) [![Bugs](https://sonarcloud.io/api/project_badges/measure?project=prodtest-demo&metric=bugs&token=9005461108c59dec16be6c57760d5aaaea5d6564)](https://sonarcloud.io/summary/new_code?id=prodtest-demo) [![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=prodtest-demo&metric=security_rating&token=9005461108c59dec16be6c57760d5aaaea5d6564)](https://sonarcloud.io/summary/new_code?id=prodtest-demo)

![Github pipeline status](https://github.com/brdv/prodtest-demo/actions/workflows/pr-flow.yaml/badge.svg)

# Prodtest demo

This is the readme for the prodtest demo project. For more information, check the [docs](https://brdv.github.io/prodtest-docs).

The current project architecture is as follows:

![Project Architecture](./assets/simple-architecture-dl.png)

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

3.  Build all docker images (locally)

    Run the build scripts

    ```bash
    # from the projects root folder:
    sh ./scripts/docker/latest.build.sh
    sh ./scripts/docker/next.build.sh
    ```

4.  Apply infrastructure resources:

    Run setup-infra script

    ```bash
    # from the projects root folder:
    sh ./scripts/kubernetes/setup-infra.sh
    ```

    **NOTE**: Because it takes some time for the infrastructure to be completed; the script waits 30 secs.

5.  Apply other kubernetes resourcess

    ```bash
    # from the projects root folder:
    sh ./scripts/kubernetes/setup-darklaunch.sh
    ```

    This will apply all resources to the cluster. You will see all resources in the console. (Run `kubectl get all` if they do not show up.)

6.  Go to http://localhost/api/health, you should see a json object as follows:

    ```json
    {
      "health": "ok"
    }
    ```

7.  To see the traefik dashboard, run the following command:

    ```bash
    kubectl port-forward $(kubectl get pods --selector "app.kubernetes.io/name=traefik" --output=name) 9000:9000
    ```

    Leave the shell open and go to http://localhost:9000/dashboard/

## Other commands

There are a few other commands that can be helpfull during testing or development.

1. Cleanup kubernetes and helm charts

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

3. Simulate API requests

   In order to simulate API requests, you can use the script `simulate_api_calls.sh`. This script will by default call the api 500 times and save the responses to `temp/request_output.json`.
   Use the script as follows:

   ```bash
   sh ./scripts/simulate_api_calls.sh
   ```

   \* _In case of an error like 'jq command does not exist' [install jq](https://stedolan.github.io/jq/download/)_

4. Check KitchenService logs

   Make sure you posted at least one order or ran the simulation script.

   ```bash
   kubectl logs deployments/kitchen-service-latest
   kubectl logs deployments/kitchen-service-next
   ```

   If the setup went right, you should see a HandledOrder object with the same OrderId on both services. The Id is different and the handler as well (to demonstrate the dark launch).
