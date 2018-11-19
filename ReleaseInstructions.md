# Release Instructions #

These instructions are meant for release managers only and should be followed when performing a release.

1. Checkout the `develop` branch locally.
2. Perform a local build (this is important for later).
3. Ensure the `Version.txt` file is up to date.
4. Open the `CHANGELOG.md` file and change the "Unreleased" heading to the version number in the format `v1.0`.
5. Commit the change with the message `Finalising release v1.0` using the same version number.
6. Push the commit.
7. Checkout the `master` branch locally.
8. Merge the `develop` branch into the `master` branch. DO NOT PUSH YET!
9. Tag the merge commit to `master` with the release name in the format `v1.0` and push the tag.
10. Go to the [github releases page](https://github.com/dale2507/KeePassRunAsPlugin/releases) and click the tag.
11. Click "Edit tag".
12. Enter the release name in the format `v1.0`.
13. Enter `# Changelog #` in the description field, then copy the contents of the `CHANGELOG.md` file below.
14. Attach the `RunAsPlugin.plgx` file from the root of the repository to the release (created from the build in step 2).
15. For pre-releases, check the "This is a pre-release" checkbox.
16. Click save draft and check the entry to ensure it is correct.
17. Go back to the edit page and click "Publish release".
18. Push the master branch to Github.
19. Checkout the `develop` branch locally.
20. Open the project properties, open "Assembly Information..." and increment the version number.
21. Perform a build to update the `Version.txt` file.
22. Commit the updated `AssemblyInfo.cs` and `Version.txt` with the message `Updated version numbers for release v1.0` using the new version number.
23. Push the `develop` branch.