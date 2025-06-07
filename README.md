# Elecciones

This application uses SQLite for election data and MongoDB for user authentication.

## MongoDB setup

Provide a MongoDB connection string using the `MONGODB_CONNECTION` environment variable before launching the app. The authentication service will use this string to connect and store users in the `elecciones` database.

## Authentication

Users are stored in MongoDB with passwords hashed using BCrypt. On first launch a login screen is presented. After a successful login the username is saved and the next time the app starts it navigates directly to the main page.

Each user document now includes a `Dni` property stored in the `dni` field.
This value is collected during registration to uniquely identify the voter.

## Registration screen

The app now contains a dedicated **RegisterPage** that collects username, DNI
and password. New users are stored in MongoDB and, on success, the main page is
shown immediately.

## Voting analysis

From the list of voters you can open an **Análisis** screen that displays the
total number of voters, how many have voted and the percentage of participation.
