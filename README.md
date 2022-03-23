<h1 align="center">Welcome to AsanaToCosmoDB 👋</h1>
<p>
  <a href="https://twitter.com/CloudyOne" target="_blank">
    <img alt="Twitter: CloudyOne" src="https://img.shields.io/twitter/follow/CloudyOne.svg?style=social" />
  </a>
</p>

> This tool allows you to export Projects, Tasks, Users, and Attachments from Asana and Import them into CosmoDB, using Azure Storage for Attachments (cheaper).

## Pre-requisites

Dotnetcore3.1 SDK <a href="https://dotnet.microsoft.com/en-us/download/dotnet/3.1">https://dotnet.microsoft.com/en-us/download/dotnet/3.1</a>

## Usage

In order to use this code, you will need to update the following information inside of `appsettings.json`:
* Asana Personal Access Token
* Asana Workspace Id
* Azure CosmoDB URI
* Azure CosmoDB PrimaryKey (Cosmo DB container will be created on it's own inside of this CosmoDB resource)
* Azure Storage Connection String
* Azure Storage Container Name (This container must be created in advanced)

## Notice

The Asana API has a Rate Limit of 150/minute for `Free` API accounts. I have handled ensuring you will never run into that limit in the code. If you have a `Premium` account feel free to remove that line of code <a href="https://github.com/CloudyOne/AsanaToCosmoDB/blob/cac6d7b3f3b0e5625abbe67969a6af581240bfed/Services/AsanaServiceBase.cs#L112">here</a>

## Author

👤 **CloudyOne**

* Website: https://teamofprogrammers.com
* Twitter: [@CloudyOne](https://twitter.com/CloudyOne)
* Github: [@CloudyOne](https://github.com/CloudyOne)
* LinkedIn: [@cloudyone](https://linkedin.com/in/cloudyone)

## Show your support

Give a ⭐️ if this project helped you!

Buy me a coffee! ☕
ETH BTC DOGE LTC: ```cloudyone.eth```

***
_This README was generated with ❤️ by [readme-md-generator](https://github.com/kefranabg/readme-md-generator)_
