using System;
using System.Collections.Generic;

namespace KMChartsUpdater.DAL.Entities
{
    public class Account : Entity
    {
        public string Login { get; set; }

        public string LoginNormalized { get; set; }

        public string Username { get; set; }

        public string VkToken { get; set; }

        public string VkId { get; set; }

        public string Organization { get; set; }

        public string Salt { get; set; }

        public string PasswordHash { get; set; }

        public int AccessLevel { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime LastLoginDate { get; set; }

        public string SessionKey { get; set; }

        public virtual ICollection<AudioTask> AudioTasks { get; set; }
    }
}
