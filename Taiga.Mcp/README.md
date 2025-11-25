


Add to cursor/settings.json :

"mcpServers": {
    "taiga-mcp": {
        "type": "stdio",
        "command": "taiga-mcp",
        "env": {
                "TAIGA_API_URL": "https://taiga-server-url.com",
                "TAIGA_USERNAME": "username",
                "TAIGA_PASSWORD": "password"
        }
    }
}

