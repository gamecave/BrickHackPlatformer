var plugin = {
    GetInput : function()
    {
        GAME_CAVE_GET_USER_INPUT();
    }
};

mergeInto(LibraryManager.library, plugin);