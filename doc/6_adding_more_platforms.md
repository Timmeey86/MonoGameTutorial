# Adding builds for Windows, Linux and MacOS

Now that we have our Android release pipeline established, it is time to build the desktop platforms as well. Don't worry, this is significantly easier. The good news is: We can build for Linux, Mac and Windows on the Mac, so we can build it on the same agent as Android.

First, let's add a variable to the `azure-pipelines.yml` which stores the path our .exe will end up in after building (unless you changed that in your project settings):

    # Configure paths here
    variables:
      # ...
      exeSourceDirectory: 'MonoGameTutorial.Desktop/bin/DesktopGL/AnyCPU/release'

Add a task for building the project anywhere in the `steps` section. Since this tutorial does not use any processor-specific libraries, we'll build the `AnyCPU` configuration.

    # Build the desktop project as a regular MSBuild project
    - task: MSBuild@1
      inputs:
        solution: '**/*Desktop.csproj' 
        platform: AnyCPU
        configuration: release
        
Unlike the android target, this will result in several files. In order to make everyone happy, let's provide them as a `.zip` for Windows and `.tar.gz` for Linux/Mac. We set `includeRootFolder` to false as otherwise the zip file would contain a folder named `release` within the zip file. Feel free to rename that directory first and then include it in the Zip (this is not explained here).

    # Zip the exe files as .zip and .tar.gz
    - task: ArchiveFiles@2
      inputs:
        rootFolderOrFile: '$(exeSourceDirectory)' 
        includeRootFolder: false
        archiveType: 'zip'
        archiveFile: '$(outputDirectory)/MonoGameTutorial.zip' 
    - task: ArchiveFiles@2
      inputs:
        rootFolderOrFile: '$(exeSourceDirectory)'
        includeRootFolder: false
        archiveType: 'tar'
        tarCompression: 'gz'
        archiveFile: '$(outputDirectory)/MonoGameTutorial.tar.gz' 
    
We want those archives to be available in the GitHub release as well, so let's upload them each as an own artifact:

    # Publish artifacts for Windows/Linux versions
    - task: PublishPipelineArtifact@0
      inputs:
        artifactName: 'WindowsArtifact'
        targetPath: '$(outputDirectory)/MonoGameTutorial.zip'
    - task: PublishPipelineArtifact@0
      inputs:
        artifactName: 'LinuxArtifact'
        targetPath: '$(outputDirectory)/MonoGameTutorial.tar.gz'
        
        
This is enough YAML configuration to get a zip which allows executing the game on Windows directly (after extracting the archive), and allows running it on Linux/Mac after unpacking, installing `mono-complete` and running it with `mono MonoGameTutorial.Desktop.exe`.
It would obviously be more practical for your users if you provided an installer later on.

In order to get those files into the GitHub tag, go back to Azure pipelines and deploy the two new artifacts:

 1. Click on `Releases`.
 2. Click on `Edit`.
 3. Click `+Add` in the `Artifacts` section, then add your two `WindowsArtifact` and `LinuxArtifact` artifacts as described in [chapter 5 (step 5)](5_publishing_a_build.md#setting-up-a-manual-release-pipeline).
 4. Click the `1 job, 1 task` link in the `Publish to GitHub` box.
 5. Click the `GitHub Release (edit) panel` and scroll down to `Assets`.
 6. Extend the list by `WindowsArtifact/*` and `LinuxArtifact/*`, each in a new line.
 7. Click `Save`, then create a new release.
