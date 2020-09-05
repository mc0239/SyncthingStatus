namespace SyncthingStatus.Data
{
    class ConfigResponse
    {
        public int Version { get; set; }

        public FolderResponse[] Folders { get; set; }
    }

    class FolderResponse
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public string Path { get; set; }
    }
}
