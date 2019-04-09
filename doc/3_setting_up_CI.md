# Setting up Continuous Integration
## Why Azure DevOps Pipelines?
This guide uses Microsoft Azure DevOps Pipelines as a continuous integration server, since it is [free for open source projects](https://azure.microsoft.com/de-de/blog/announcing-azure-pipelines-with-unlimited-ci-cd-minutes-for-open-source/) and provides a MacOS image with Xamarin preinstalled: This allows building both iOS and Android applications within minutes.
At the time of writing this guide, Travis CI offers building on Mac as well, but requires manually downloading and installing the Android SDK and other tools which requires way more time and effort
*If this ever changes, or if you know another valid alternative, please extend this guide with that alternative.*

## Setting up an Azure DevOps Pipelines account

 1. Go to [https://azure.microsoft.com/en-us/services/devops/pipelines/](https://azure.microsoft.com/en-us/services/devops/pipelines/).
 2. Click `Start free with Pipelines`.
 3. Log in with your microsoft account, or register one. You can use any e-mail address, it doesn't have to be a hotmail one.
 4. Read through the terms of use and code of conduct if you wish, then click `Continue`.
 5. Assign a project name and be sure to set the `Visibility` to `Public`, otherwise it will not be free for long. Then create the project.
 6. When being asked where your code is, select `GitHub`.
 7. Select the repository you created, in this case `MonoGameTutorial`.
 8. Chose `Starter pipeline`.
 9. In order to get familiar with the tool just click `Save and run` now two times. Wait till the build finished, then click through the logs and get to know things.
 
At this point, the pipeline is ready to build (but not deploy) our game. However, we need to let it know how to build it properly. 
We'll explain the general thing by building Android on Mac.

## Configuring the YAML file for building Android

First, let's get the checked-in YAML file into our `setup` branch. The file was added by Azure to the master branch and only on GitHub so far. Open a `Git Bash` in your local repository root and type (assuming you still have the branch checked out):

    git fetch
    git merge origin/master
    git push

Dependent on your settings, the merge might give you the option to adapt the commit message. Either way, at the end you'll have the azure-pipelines.yml in your local branch, and will have committed that into your GitHub branch.

Now it's time to edit this very file, section by section. First, change the `trigger` section to:

    # Trigger the Azure DevOps build upon changes in the master or setup branches
    trigger:
    - master
    - setup

This will auto-trigger the azure pipeline on every commit into the setup branch. Note that azure always only inspects the YAML file in the changed branch. We add `master` anyway so this still works when merging our `setup` branch back to master.
Change the `pool` section to (*Note: Never use tabs in YAML, only indent with two spaces, or it won't parse*)

    # Build on MacOS 
    pool:
      vmImage: 'macOS-10.13'

Add a new `variables` section and make sure to specify your own package name instead of mine:

    # Configure paths here
    variables:
      buildConfiguration: 'Release'
      outputDirectory: '$(build.binariesDirectory)/$(buildConfiguration)'
      packageName: 'com.github.timmeey86.monogametutorial'

Remove everything below `steps` and add the following content to install `NuGet`:

    # Install NuGet
    - task: NuGetToolInstaller@0

As a next step, we can have NuGet download all managed dependencies via:

    # Restore any missing dependencies
    - task: NuGetCommand@2
      inputs:
        restoreSolution: '**/*.sln'

If you wondered, @0 and @2 indicate the task version, which allows Microsoft to release new versions without breaking existing scripts.

The Mac build slave comes with a preinstalled Xamarin Studio, but is still missing Monogame. The easiest way is to simply download it and run a silent install:

    # Install MonoGame and other dependencies
    - script: |
        # Download Monogame 3.7.1 (-L is important so curl follows redirects)
        curl -L -O https://github.com/MonoGame/MonoGame/releases/download/v3.7.1/MonoGame.pkg
        # Install the MonoGame package
        sudo installer -pkg "$(pwd)/MonoGame.pkg" -target /
      displayName: 'Installing MonoGame'

Now we're ready to actually build our .apk:

    # Build any .csproj containing "Droid" as a Xamarin project
    - task: XamarinAndroid@1
      inputs:
        projectFile: '**/*Droid*.csproj'
        outputDirectory: '$(outputDirectory)'
        configuration: '$(buildConfiguration)'

and publish it so it can be reused by a release pipeline. The artifact name is like a label (and a folder name) for your artifact, so name it anything you want.
    
    # Publish an artifact to be used for Android
    - task: PublishPipelineArtifact@0
      inputs:
        artifactName: 'AndroidArtifact' 
        targetPath: '$(outputDirectory)/$(packageName).apk'

You can now commit this through

    git add .
    git commit -m "First attempt at building an APK"
    git push

Check your azure pipeline account, it should start building the commit after around 10-20 seconds.

Before actually uploading that artifact anywhere, though, we will talk about signing an Android APK (this is vital if you ever want it to be in the Play Store).

[Continue with the guide](4_signing_an_apk.md)
