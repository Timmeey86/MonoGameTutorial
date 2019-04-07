# Signing an Android APK within Azure Pipelines

This process is somewhat tedious, but you will only ever have to do it once for each App you develop.
First, we need to create a keystore which contains a key to be used for signing through the `keytool` command line tool which should be available if you installed any recent JDK.

Before executing the tool, however, you need to think about two passwords which you should store in a personal password manager or another safe place: A password for accessing the keystore, and another password for accessing the key which will be used for signing.
[Tips on safe passwords](https://www.google.com/search?q=tips+on+safe+passwords]) (We never want to change these two, however)

## Creating the keystore file
Once that's done, execute the `keytool` in a command line, but replace `<storePassword>` and `<keyPassword>` by your two keys. `<filename>` can be anything you want, in this example, it could be `MonoGameTutorial.keystore`.

    keytool -genkey -v -keystore <filename> -alias uploadkey -storepass <storePassword> -keypass <keyPassword> -keyalg RSA -validity 10000

Fill in the information you are being asked, then type "Yes" (in your language) to confirm.
This will generate a key (unknown to you) which is protected by your `<keyPassword>` and place it into a `<filename>` keystore which is protected by `<storePassword>`. As long as you always use this key for all versions of your app, you will be able to update it in Play Store with maximum security. The key will be valid for 10.000 days which is more than 27 years.
Speaking of security: It would be very unsafe to upload the keystore to GitHub, since people could try to hack it. Fortunately, Azure Pipelines offers a free way for storing it with an additional encryption on their servers, then download and decrypt them within the build.

## Securing the keystore file and passwords
In order to do that, go back to your Azure Pipelines project and within the `Pipelines` section,  click on `Library`->`Secure Files`, then upload your Keystore file there. Once uploaded, click the file in the overview, check the `Authorize for use in all pipelines` setting and click the `Save` button.
Make sure you **do not** accidentally add this file to GitHub.

In order to be able to safely decrypt the file during the build, you need to additionally store your passwords as encrypted variables. The best place to do this is in the configuration of your pipeline. Click the `Pipelines` section again, click the `Edit` button in the top-right corner, then click the three dots, then `Variables`.
Add three variables `keystore.pass`, `keystore.alias` and `keystore.aliaspass`. The alias is `uploadkey` if you did not change it. It would not require encryption, but why not manage it in the same place as the passwords.
Click the lock symbols for each of the variables to store them as secrets.

## Signing the APK with the encrypted keystore and variables
Edit your `azure-pipelines.yml` and add the following section before the publishing one:

    # Sign the app using the decrypted keystore and the passwords stored in the Azure Pipeline
    - task: AndroidSigning@3
      inputs:
        apkFiles: '**/*.apk'
        apksign: true
        apksignerKeystoreFile: 'MonoGameTutorial.keystore'
        apksignerKeystorePassword: '$(keystore.pass)'
        apksignerKeystoreAlias: '$(keystore.alias)'
        apksignerKeyPassword: '$(keystore.aliaspass)'
        zipalign: true

The `apksignerKeystoreFile` parameter is the name of your keystore file. We are being lazy and search any apk in any subfolder with `'**/*.apk'`. Commit and push the changes, you should know how to do that by now.

Congratulations, you signed your first APK with your own key. However, you can not download it or anything, so you need to publish it somewhere.

[Continue with the guide](5_publishing_a_build)
