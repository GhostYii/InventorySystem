public static class Tags
{
    public static readonly string Untagged = "Untagged";
    public static readonly string Respawn = "Respawn";
    public static readonly string Finish = "Finish";
    public static readonly string EditorOnly = "EditorOnly";
    public static readonly string MainCamera = "MainCamera";
    public static readonly string Player = "Player";
    public static readonly string GameController = "GameController";
    public static readonly string UICamera = "UICamera";
    public static readonly string DisplayItem = "DisplayItem";

    private static string[] tags = {
                                      "Untagged",
                                      "Respawn",
                                      "Finish",
                                      "EditorOnly",
                                      "MainCamera",
                                      "Player",
                                      "GameController",
                                      "UICamera",
                                      "DisplayItem",
                                  };

    public static bool HasTag(string tag)
    {
        foreach (string item in tags)
        {
            if (item.Equals(tag))
                return true;
        }

        return false;
    }
}
