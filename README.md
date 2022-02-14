# FancyDiscordBot

<img src="https://user-images.githubusercontent.com/79592738/153656594-2d2ae833-edde-4389-a10f-7aa6ded0732a.png">

A Discord Bot made in .NET 6 with [DSharpPlus](https://github.com/DSharpPlus/DSharpPlus). <br/>
Main porpuse of this bot was to have fun with some friends.

The bot can:
* execute different commands, that are requested with the "fancy" prefix (to see all command use fancy help)
* react to different words. Eg: when a user types "f" in the chat, the bot with also send a "f" in the chat
* detects when a user joins a server and gives him a fancy greeting
* detects when a user leaves the server and sends a message
* some commands can send private messages
* some commands can send embed messages
* can search on google requested images
* can search on websites to find data, eg. find jokes
* uses an api to send dadjokes

Hand-made features:
* A Command Manager to choose the current command in a efficient way (dsharpplus has it's own variant, but it didn't fit my needs)
* Uses Reflection to search for command, a command is represented by a class that implements IDiscordCommand and has a Command attribute, so I don't need to add them manually to main controller
* IoC containner for dependency injection when creating commands

The bot is hosted on Azure Servers as a WebJob.
If you are interested in deplying a .NET discord bot to azure, [this](https://swimburger.net/blog/azure/creating-a-discord-bot-using-net-core-and-azure-app-services#deploying-to-azure-app-service-webjobs) tutorial may be useful. It's really pretty simple.

Eg. of embed message

![image](https://user-images.githubusercontent.com/79592738/153651703-b4a6bddf-fa96-448d-976f-8a065d960599.png)

---

To create your own discord bot first got to [Discord Developer Website](https://discord.com/developers/applications) and create a new application.<br/>
To use my code for your bot, clone or download source code and add a config file named "appsettings.json" (path can be change from main, in configuration builder) with the following content:

```json
{
  "discordtoken": "YOUR BOT TOKEN HERE",
  "dadjokekey": "YOUR DAD JOKES API KEY HERE"
}
```

You can also use secrets instead of a config file, to use secrets you must modify the config manager to use secrets. <br/>
DO NOT leave your tokens on a public repository.

Hope this was useful!
