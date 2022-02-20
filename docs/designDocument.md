# The Unity Cloud build system design document

Based on the requirements, the application that needed to be built was a command line application that other developers could use to do the following:

- Clone a Unity project source code from a source control manager.

- Build the project to either Android or iOS using the Unity Editor.

- Zip and copy the build logs and artifacts to a location of my choice.

The command-line application has been designed with an argument parser. This allowed to quickly add support for specific actions that tool needed to support. From the entry point of the application (the _Main_ method in _Program.cs_), we have here a very explicit way of detailing how the user arguments should be converted and the expected behavior once we found a supported command. By following this approach, we have now a linear design in terms of the commands need to implement and how they are being parsed.

Due to the distinct nature of the features, each of the commands the application support has been designed to exists in a silo. There was no reason to couple together the behavior to clone a repository and to zip a set of files together for instance. Doing so allows to add further implementation support to the existing features without fearing of breaking the behavior for other features.

The application's development initially started with setting up the cloning feature. Since there weren't any requirement other than pick one source control manager (_SCM_) and that the project was already under Git, the tool uses Git as a SCM. The way the application has been implemented makes the tool currently coupled only to Git as SCM and would need some considerations to make it more robust to changes when we'd be adding support to other systems such as Perforce. 

For the future, it would be best to look at the SCMs that the CLI would need to support and find an abstract way of executing the behavior that a specific SCM can do. For instance, the CLI feature _download project_ could relate to the ```git clone``` and the ```p4 sync``` if we were to extend to support Perforce. That would require the developer to specify the SCM they wish to use. That could either be something that could be passed as an argument or the CLI could define a user profile and let the developer set that value whenever they want.

I believe in the future for the tool, it would be good to add the following features:
- Fetch
- Pull
- Clean

The reason why I believe so is that this tool is meant to automate as much as possible how we can build the game on in the cloud. One thing that need to be certain of is that we're building the game in a clean environment. This would improve the level of confidence someone would have with respect to repeatable builds that show we can build the game with the same level of quality every single time.

Also, a discovery was made during development that forced the way to code had to be implemented to make sure the feature wouldn't break for certain developers. When cloning the repository, I had the unfortunate issue to see that I couldn't clone it due to my default branch's name not being set.

> Starting in Git 2.28, git init will instead look to the value of init.defaultBranch when creating the first branch in a new repository <br/>
> source: https://github.blog/2020-07-27-highlights-from-git-2-28/

Moving forward to building the game. The requirement asked to either build the game for Android or iOS. In terms of the development, the infrastructure responsible for setting the active build target will validate with a hardcoded set collection whether the provided target is valid. The reason why it's a set is due to the O(1) nature of the ```Contains``` operation and if we had a very large collection to check against, the validation would still be very fast. The decision intended for some coupling based on the documentation and were it to change, it would be trivial to make the necessary adjustments.

While going through the documentation, I discovered how to run Unity in batch mode and also, how to build the player for the game. From the documentation, I found out that to make the operation successful, it was required to have a script in the game project responsible for building the player. In the development phase, I added such a script to my local repository of the 2D platformer.

On the performance side of things, the publishing feature might take some time to complete. At least, the API supports getting multiple directories from which we can build a single file archive. Where the problem would lie would be in the file structure of the game repository. If the project is fairly large, it would take some time due to the nature of the search in the all the directories. If that were the case, it would be possible to reevaluate the search pattern and see how we would best improve the overall performance of the search.

Finally, to be respectful of the time allocated for the exercise, no unit tests were added. This wouldn't be the sort of tool to be put in production. First, I'd start by adding a test project to validate the specific requirements for each feature. Furthermore, since we're interacting with the file system with this tool, leveraging the NuGet package [System.IO.Abstractions](https://www.nuget.org/packages/System.IO.Abstractions) would make it possible to create unit tests for the IO.