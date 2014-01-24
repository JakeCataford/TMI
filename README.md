TMI
===

TMI (TileMap Interface) is a Unity plugin for handling 2d tilemaps in a native way. 

####Installation

Just put the TMI folder into your project, I will make it into a package in the near future.

####Usage

TMI consists of three main components.

`TilemapManager` is a singleton that handles creating and adding layers, and acts as an access point for everything tile related in the app.

`Tilemap` Objects are responsible for storing information about a tilemap, so which tileset to use, and what indexes go where in the context of the tilemap. this object also is responsible for rendering the tiles from the tileset when `Render()` is called. It attaches the newly instantiated objects to it's transform as children and is responsible for blowing them away when done.

`Tileset` is a definition for a Tileset. At the moment it supports solid and non solid tiles, add it to an empty game object and add tile sprites in the inspector. Clicking on a sprite will toggle it's solid state.

Those components make up the brunt of the tilemap API. When the spec settles, I will further document everything.




