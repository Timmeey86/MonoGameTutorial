# Setting up Continuous Integration for a new open source cross-platform MonoGame project 

## About this Guide
This guide currently explains how the following things can be done for free for an **open source** project:

 - Setting up a cross-platform MonoGame solution within Visual Studio 2017 for the following target platforms:
	 - Universal Windows Platform (UWP)
	 - Linux
	 - MacOS
	 - Android
	 - iOS
 - Setting up a Continuous Integration build pipeline for all target platforms.
 - Securely digitally signing a file (e.g. Android APK file) using a java key store.
 - Setting up a Continuous Integration release pipeline which uploads the build artifacts to a GitHub tag.

The guide is a detailed step-by-step explanation so you don't need too much knowledge in advance, but setting all these things up will still cost you several hours. It cost me several days trying to figure things out, so I hope I can give something back to the community with this guide.
 
## Exclusions
This guide will not explain how to:
- set up developer accounts for Google Play Store, Apple App Store or the Microsoft Store.
- set up a Continuous Delivery pipeline which publishes into the various stores directly.

## Prerequisites:
- A Windows 10 machine
- Visual Studio 2017 (other versions might work, too) with at least the following packages being installed:
  - .NET Desktop Development
  - Mobile Development with .NET
  - Optional: Development for the Universal Windows Platform and activated Developer settings in the windows settings (Those that allow executing any app even of untrusted sources).
- An installed MonoGame with working templates
- A GitHub account
- An installed Git Bash shell or TortoiseGit
- 
## Getting Started
[Setting up a cross-platform MonoGame solution](2_setting_up_a_solution)
