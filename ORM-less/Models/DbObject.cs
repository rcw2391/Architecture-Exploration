﻿namespace Models
{
    public class DbObject : IDbObject
    {
        public bool IsDirty { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsNew { get; set; }
    }
}
