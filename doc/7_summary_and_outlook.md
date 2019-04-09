# Summary

Following this guide, you have (hopefully) successfully done the following things:

- Set up a sample (or an actual) cross-platform project using `MonoGame`.
- Set up a continuous integration/deployment pipeline using `Azure DevOps`.
- Created your first `GitHub` release which contains an Android APK, a Windows Zip File and a Linux/Mac Tarball.
- Gained an understanding of how you can continue from here.

# Outlook

Next steps you could take from this point:

- Create a Pull Request from your branch on GitHub and merge it back to your `master` (you can also do this on the command line of course). When doing it on GitHub, I recommend picking the `Squash and Merge` option from the dropdown after creating and before acceppting the pull request.
- If you haven't done already, set up an actual project for your app.
- Set up a Unit Testing / other testing framework and integrate it into your Continuous Integration pipeline - The less effort it takes to write a test when you need it, the more likely you are going to write it. So why not set this up right now?
- Add more platforms to your project and the release pipeline.
- Set up developer accounts in the various app stores and modify your release pipeline to publish directly into the stores.
- Decide that you're not going to need any of these and start coding/deciding/documenting/whatever.

# Extension of this guide:

This guide is currently missing at least the following topics. If you are an expert at these things, why not give something back to the community and extend this guide?

- Building for iOS / Apple App Store
- Options for developing for iOS on Windows (assuming you own an additional Mac/Mac Mini/a VM with Mac that suddenly appeared on your PC).
- Known Cross-Platform differences you should keep in mind
- Things to keep in mind when developping for both desktop and mobile (e.g. Desktop and UWP with XAML)
- Valid alternatives to Azure DevOps
