# Serious Scavenger Hunt
Serious Scavenger Hunt is a free, open source web applicaiton for building, running and managing great scavenger hunts.

## Testing
This website is being continuously deployed to [Windows Azure](https://serioushunt.azurewebsites.net). You can try the latest version of the code.

The database is re-created with sample data at each deployment.

### Demo users
There are 2 user/roles in this app:

Username | Password | Role | Description
:-- | :-- | :-- | :--
admin | admin123 | Admin | Create new stunts, manage teams
judge | judge123 | Judge | Judge stunts

Of course you can also create your own users (some OAuth providers are supported).

## Development

1. Clone code from git repository
2. Open in Visual Studio
3. Press `F5`

## Deployment

You'll need to provide some data to the `web.config` file in order to deploy successfully.

Section | Key | Required | Description
:-- | :-- | :-- | :--
ConnectionStrings | ScavengerHuntContext | Yes | Connection string to your SQL Server
AppSettings | FacebookAppId | No | ID of your Facebook app
AppSettings | FacebookAppSecret | No | Secret key of your Facebook app
AppSettings | TwitterAppId | No | ID of your Twitter app
AppSettings | TwitterAppSecret | No | Secret key of your Twitter app
AppSettings | MicrosoftAppId | No | ID of your Microsoft app
AppSettings | MicrosoftAppSecret | No | Secret key of your Microsoft app 