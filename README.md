# Elecciones

This application uses SQLite for election data and MongoDB for user authentication.

## MongoDB setup

Provide a MongoDB connection string using the `MONGODB_CONNECTION` environment variable before launching the app. The authentication service will use this string to connect and store users in the `elecciones` database.

## Authentication

Users are stored in MongoDB with passwords hashed using BCrypt. On first launch a login screen is presented. After a successful login the username is saved and the next time the app starts it navigates directly to the main page.
