# Setting up Continuous Integration for a new open source cross-platform MonoGame project 

## About this Guide
When I (Timmeey86) started my research on how to build cross-platform games with heavy use of continuous integration, I was (negatively) surprised I couldn't find a guide for this. There were several guides on how to port from one platform to another, or how to set up CI for a single platform, but none on how to start cross-platform right away.
In my experience, it is almost impossible to do the right architecture decisions if you first develop for one platform and then port to another - unless you know every requirement for every platform you are going to support already. Additionally, I prefer being able to test my game on any supported platform whenever I want with minimal setup time.
These are just two of the major reasons why I think the effort for going Cross-platform from the start and going CI from the start is justified.

This guide is my attempt at assisting the like-minded and reducing the effort required for setting everything up, while at the same time documenting how I did things for my future self. It is not a "copy this complete config into your repo and you're good to go" guide, but rather a detailed step-by-step explanation which should give you a completely working example plus enough knowledge to improve that for your needs.

This guide currently explains how the following things can be done for free for an **open source** project:

 - Setting up a cross-platform MonoGame solution within Visual Studio 2017 for the following target platforms:
	 - Linux
	 - MacOS
	 - Windows
	 - Android
	 - In future: iOS + Universal Windows Platform (UWP)
 - Setting up a Continuous Integration build pipeline for all target platforms.
 - Securely digitally signing a file (e.g. Android APK file) using a java key store.
 - Setting up a Continuous Integration release pipeline which uploads the build artifacts to a GitHub tag.
 
## Exclusions
This guide will not explain how to:
- set up developer accounts for Google Play Store, Apple App Store or the Microsoft Store.
- set up a Continuous Delivery pipeline which publishes into the various stores directly.
- actually develop a game

## Prerequisites:
- A Windows 10 machine
- Visual Studio 2017 (other versions might work, too) with at least the following packages being installed:
  - .NET Desktop Development
  - Mobile Development with .NET
  - Optional: Development for the Universal Windows Platform and activated Developer settings in the windows settings (Those that allow executing any app even of untrusted sources).
- An installed MonoGame with working templates
- A GitHub account
- An installed Git Bash shell or TortoiseGit

## Getting Started
[Setting up a cross-platform MonoGame solution](2_setting_up_a_solution.md)
