
# Prodtest demo
This is the readme for the prodtest demo project. For more information, check the [docs](https://brdv.github.io/prodtest-docs).

## Contents
The contents of this Readme are:
- [Prerequisites](#prerequisites)
- [Installation](#install-and-run)

## Prerequisites
In order to run the project, please make sure the following tools are installed:
- [Docker](https://docs.docker.com/get-docker/)
- [Minikube](https://minikube.sigs.k8s.io/docs/start/)
- [Kubectl](https://kubernetes.io/docs/tasks/tools/)
- [Dotnet](https://dotnet.microsoft.com/en-us/download) 

\* Please use [WSL](https://learn.microsoft.com/en-us/windows/wsl/install) when you are using windows

## Install and run
Once you meet all the prerequisites you need to start a minikube cluster:
```bash
# check if kubectl and minikube are installed correctly by:
kubectl version
minikube version

# start minikube cluster:
minikube start
```
This will setup a local kubernetes you can use to demo this project.  Once your minikube cluster is up and running, you can create all resources and start the project by entering the following in your terminal:
```bash
# from the projects root folder:
sh ./scripts/kubernetes/setup-darklaunch.sh
```

A browser will open with the base url for the API. Add `/api/health` to the end of the url to see if it's working correctly. (Note: issue will be opened for default launch to endpoint.)