# Publishing a Build to GitHub
In order to publish a build to GitHub you need two things:
- A GitHub release to publish to
- A release pipeline in Azure Pipelines

## Preparing a GitHub release

 1. Go to your GitHub repository and click the `0 release`link somewhere in the middle.
 2. Click `create a new release`.
 3. Enter a release number like `0.1.0.0` and select your `setup` branch (created in part 2).
 4. Give it a name like `Pipeline Release`, check `This is a pre-release` and click `Publish release`.

## Setting up a Manual Release Pipeline
In this section, we set up a manual release pipeline which publishes to the same tag.

 1. Go back to Azure Pipelines and click `Pipelines`->`Releases`.
 2. Click `New Pipeline`.
 3. Click `Empty job` rather than selecting a template.
 4. Click `Add an artifact`.
 5. Select the only entry in the `Source (build pipeline)` artifact, then change `Source alias`to the artifact name you picked in step 3. In this case `InitialTestRelease`. Click `Add`.
 6. In the `Stage 1` box, click the link `1 job, 0 task`.
 7. Change the stage name to e.g. `Publish to GitHub`, then click on the `+` sign in the `Agent job`panel.
 8. Select `Utility` and add a `GitHub Release` there.
 9. Click the new panel and adjust the following settings:
	 - Click `+ New`, then `Authorize using OAuth`.
	 - Select your repository.
	 - Set action to `Edit` (we already created the GitHub release).
	 - Enter the tag name you created a couple of minutes ago, e.g. `0.1.0.0`.
	 - Change `Assets` to `InitialTestRelease/*` (Use the name you assigned in step 5).
	 - Check `Pre-release`, then click `Save` and `OK`.
	 - Click on `+ Release`and `Create Release` and `Create`.
	 - Click on `Releases` in the left-hand panel.
	 - Once you see a green check mark next to `Publish to GitHub`, you can go to GitHub and you should see your signed APK file in the release you just created.
