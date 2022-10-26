using SocialMedia.Business.Abstract;
using SocialMedia.DataAccess.Abstract;
using SocialMedia.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SocialMedia.Business.Concrete
{
    public class PostManager : IPostService
    {
        private readonly IPostDal _postDal;
        private readonly ITagDal _tagDal;
        private readonly IPostTagDal _postTagDal;

        public PostManager(IPostDal postDal, ITagDal tagDal, IPostTagDal postTagDal)
        {
            _postDal = postDal;
            _tagDal = tagDal;
            _postTagDal = postTagDal;
        }

        //user id 
        public async Task AddPostAsync(Post post)
        {
            post.Content = post.Content.Trim();
            var postTags = Regex.Matches(post.Content, @"\#\w+").Select(a => a.ToString().TrimStart('#')).ToList();
            //var userTags = Regex.Matches(post.Content, @"\@\w+").Select(a => a.ToString().TrimStart('@'));

            await _postDal.Add(post);

            //Adding HashTags at post
            foreach (var tag in postTags)
            {
                var foundTag = await _tagDal.Get(t => t.Name == tag);
                var postTag = new PostTag { PostId = post.Id };
                if (foundTag is { })
                {
                    postTag.TagId = foundTag.Id;
                }
                else
                {
                    //If tag not exsist create new tag
                    var newTag = new Tag { Name = tag };
                    await _tagDal.Add(newTag);
                    postTag.TagId = newTag.Id;
                }
                await _postTagDal.Add(postTag);
            }
            
        }

        public async Task<List<Post>> UserPostsByIdAsync(int userId)
        {
            var posts = await _postDal.GetUserAllPostsAsync(userId);
            return posts;
        }
    }
}
