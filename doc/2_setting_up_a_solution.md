# Setting up a Cross-Platform MonoGame Solution

## Setting up a repository
*Skip this if you already have one*

 1. Log into your GitHub account at [https://github.com/](https://github.com/).
 2. Click the `+` symbol in the top-right corner, then `New repository`.
 3. Give the repository a name and description. We use "MonoGameTutorial" here.
 4. **Important: The repository needs to be set to public if you want to use free continuous integration pipelines. Closed source projects will cost you.**
 5. Pick "Visual Studio" as .gitignore, then decide about a license and if you want to start with a readme file (recommended).
 6. Click "Create repository"

## Setting up a development branch
*Note: This section assumes you have installed a Git Bash with shell integration (you can select it from the context menu in the Windows Explorer). You may use TortoiseGit as well, of course.*
1. On the web page which is still open, click the green `Clone or download` button, then copy the Github URL to the clipboard.
2. Go to the folder which shall receive the local repository as a subfolder, right click it and select `Git Bash Here`.
3. Clone (download) your repository locally by typing `git clone <paste the copied URL here> <new local folder name>`.
Example: `git clone https://github.com/Timmeey86/MonoGameTutorial.git MonoGameTutorial`
Then, `cd` into the new directory in the Git Bash: `cd MonoGameTutorial`
4. Since we are testing things, it is recommended to create a branch for the changes, which can be merged back to the `master` branch once we're satisfied:
`git checkout -b setup`
5. The branch only exists locally. Push it to the GitHub repository by doing:
`git push --set-upstream origin setup`
This will do two things: Create a new branch called `setup` within your remote GitHub repository, and connect your local branch `setup` with the remote one.

## Creating the solution and the platform targets
1. Open Visual Studio and press `Ctrl`+`Shift`+`N`.
2. Select `Other Project Types`->`Visual Studio Solution`->`Blank Solution`
3. Type in a solution name, e.g. `MonoGameTutorial`, uncheck `Create directory for solution` and pick the local repository folder as the location.
4. Right-click the solution, do `Add`->`New project...` and pick a `Shared Project` with e.g. the name `MonoGameTutorial.Shared`. Select your local repository as a location again.
5. Add a new `MonoGame Windows Project` called `MonoGameTutorial.Content` and delete everything but the `Content` folder. We use this project to share resources, it won't be a separate executable.
Therefore, right click that project, go to properties and change the output type from `Application` to `Class Library`.
6. Create three more projects:
	- A `MonoGame Cross Platform Desktop Project`, a `MonoGame Android Project` and a `MonoGame iOS Project`. Let's call them `MonoGameTutorial.Desktop`, `MonoGameTutorial.Droid` and `MonoGameTutorial.iOS`.
 7. Optionally, you can add a Windows UWP project, but this is not described in this guide.

## Fixing the Android project
 1. Create a manifest: Right-click the Android project and select `Properties`.  Pick a `Target Framework` and then go to `Android Manifest`. Click on `No AndroidManifest.xml found. Click to add one.`. Change the package name to a reverse-DNS syntax, e.g. `com.yourwebsite.yourproject`.  If you don't have a website, you could do `com.github.yourgithubname.monogametutorial`. Set the `Version name` to e.g. "0.1.0.0" and pick a few permissions you know you are going to need, then save the project.
 2. Fixing a namespace clash: In `Activity1.cs`, change the line
 `LaunchMode = Android.Content.PM.LaunchMode.SingleInstance`
 to 
 `LaunchMode = global::Android.Content.PM.LaunchMode.SingleInstance`. 
 3. You should now be able to build the Android Project. In order to deploy it, you will have to add a device through the `Device Manager`, then deploy to that device (This is not explained here).
 *Note: The resulting .apk will not be signed within our project - Azure DevOps Pipelines will do this for us.*
 
## About the iOS project
1. Developing this project is only possible if you own a Mac. Since the creator of this guide doesn't, and doesn't plan on doing so, you're unfortunately on your own from here. If you want to skip development for iOS for now, remove the project from your solution, but leave it in the repository so you or others can develop it with a Mac later.

## Preparing Code Reuse
 1. Remove the `Content.mgcb` from the three target platform projects, then add the one of the `Content` project as a link: Right-click the `Content`folder of a platform target project, chose `Add`->`Existing Item`, then navigate to the `MonoGameTutorial.Content` project, find the `Content.mgcb`, select it and add the small arrow next to the `Add` button. This allows you to add the file as a link. After adding, right-click the `Content.mgcb` file, select `Properties`,  then set `Build Action` to `MonoGameContentReference`. Repeat this for the other target platform projects.
 2. Merge the common parts of the three Game1.cs into a GameBase.cs in the shared project. It is highly recommended to do this before coding since it encourages you to keep platform specifics in the projects intended for that and keeps the complexity of the shared project lower. It is also recommended to rename any Game1.cs into things like AndroidGame.cs. Then override the GameBase class in your projects with only the platform-specific changes. You can see an example at [https://github.com/Timmeey86/MonoGameTutorial](https://github.com/Timmeey86/MonoGameTutorial).

## Commit Your Code
Go back to the `Git Bash`, or open a new one at the root of your repository, then execute the following commands (assuming you have your .gitignore set up properly, which will be the case if you chose `Visual Studio` when you created the project on GitHub):

    git add .
    git status
    git commit -m "Initial cross-platform code structure"
    git push

*Note: The `git status` is not necessary but shows which files will be committed. You usually want to validate this before each commit.

This amount of code is already sufficient to set up continuous integration from day one.

## Setting up CI
[Continue with the guide](3_setting_up_CI.md)
