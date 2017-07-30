# Contributing
This project needs you! If you found an issue, or you want to actively contribute, this is the starting point!

But wait, are you asking... Where should I start?

## The place where it starts
If you get to this point, you have my respect! You are using the tool, and that is awesome! Thanks!!

As a starting point you need to create an issue. GitHub is awesome, and for a small project like this, the issue tracker is what we need (KISS, right?).
Click on *Issues* tab on top, and open a issue (or use this [link][issues]). Kick-off the discussion and the issue will be tagged accordely.

## This tool is technical, I'm a techie, and I will enhance it!
If you get this far, you heavly use the tool and that *bug* (sometimes we call it *by design*) really annoys you. Well, it's OSS, and you want to left your mark, enhancing it.

Start forking the repo (assuming that you have an [GitHub account]). This [guide][forking_guide] will help you with the task.
Then fork the repo:

    git clone git@github.com:your-username/pullrequests-viewer.git
    
Setup your machine following this [guide][setup_guide]!

Make sure that it builds, and the tests pass:
- On Windows:
    ```ps
    ./build.ps1
    ```
- On MacOSX/Linux:
    ```sh
    ./build.sh
    ```

At this development stage you can create your own branch, or working on your master. This is not a game changer. The project uses [SemVer][semver] for release versioning.

Make your bug fix/feature enhancements, making sure the tests pass:
- On Windows:
    ```ps
    ./build.ps1
    ```
- On MacOSX/Linux:
    ```sh
    ./build.sh
    ```

Stage the changes:

    git add <file(s)>

Commit the changes, giving a [meaningful message][git_commit_messages]:

    git commit -m "My meaningful message"

Push the changes to origin:

    git push origin <branch_name>

Then you create a [Pull Request][pull_requests] with a [brief description][pr_description] of the PR. I will take a look ASAP (I'm using the tool, any enhancement is welcome), and will merge it. You have higher changes to be approved if:
- Is linked to an issue
- Is covered with [tests][tests]
- Follow the [styling suide][styling_guide]

[issues]: https://github.com/joaoasrosa/pullrequests-viewer/issues
[github_account]:https://github.com/join
[forking_guide]: https://help.github.com/articles/fork-a-repo/
[setup_guide]: TODO
[semver]: http://semver.org/
[git_commit_messages]: http://tbaggery.com/2008/04/19/a-note-about-git-commit-messages.html
[pull_requests]: https://help.github.com/articles/creating-a-pull-request/
[pr_description]: https://github.com/blog/1943-how-to-write-the-perfect-pull-request
[tests]: TODO
[styling_guide]: TODO
