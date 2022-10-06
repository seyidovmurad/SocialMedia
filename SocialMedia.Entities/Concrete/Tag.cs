using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Entities.Concrete
{
    public class Tag: IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<PostTag> PostTags { get; set; }
    }
}
