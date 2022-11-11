# Prodtest demo

This is the readme for the prodtest demo project. For more information, check the [docs](https://brdv.github.io/prodtest-docs).

## Contents

The contents of this Readme are:

- [Prerequisites](#prerequisites)
- [Setup](#setup)

## Prerequisites

In order to run the project, please make sure the following tools are installed:

- [Docker](https://docs.docker.com/get-docker/)
- [Kubectl](https://kubernetes.io/docs/tasks/tools/)

\* **Note:** Please use [WSL](https://learn.microsoft.com/en-us/windows/wsl/install) if you are on windows

## Setup

Follow the guide below to setup and start the project.

1.  Once all prerquisites are installed, make sure you have a docker kubernetes cluster running following [this guide](https://docs.docker.com/desktop/kubernetes/#enable-kubernetes).

        **Check kubectl installation**

2.  Next make sure the docker-desktop kubernetes context is selected for kubectl:

    ```bash
    # 1) get all contexts
    kubectl config get-contexts

    # 2) set docker-desktop context if not set allready *
    kubectl config set-context docker-desktop
    ```

    \* you can see if docker-desktop is selecten in the output of command 1.

3.  Last setup step: apply all resources:

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

\* more coming soon...
