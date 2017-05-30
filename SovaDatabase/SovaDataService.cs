using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DomainModel;

namespace SovaDatabase
{
    public class SovaDataService : IDataService
    {
        public PagedList<Post> GetPosts(ResourceParameters resourceParameters)
        {
            using (var context = new SovaContext())
            {
                var data = context.Posts.OrderBy(x => x.Id);
                return PagedList<Post>.Create(data, resourceParameters.PageNumber, resourceParameters.PageSize);
            }
        }

        public Post GetPost(int id)
        {
            using (var context = new SovaContext())
            {
                return context.Posts.Find(id);
            }
        }

        public PagedList<History> GetHistories(ResourceParameters resourceParameters)
        {
            using (var context = new SovaContext())
            {
                var data = context.Histories.OrderBy(x => x.UserId);
                return PagedList<History>.Create(data, resourceParameters.PageNumber, resourceParameters.PageSize);
            }
        }

        public History GetHistory(int userId, int postId)
        {
            using (var context = new SovaContext())
            {
                return context.Histories.Find(userId);
            }
        }

        public void CreateHistory(History history)
        {
            using (var context = new SovaContext())
            {
                context.Histories.Add(history);
                context.SaveChanges();
            }
        }

        public void UpdateHistory(History history)
        {
            using (var context = new SovaContext())
            {
                context.Histories.Update(history);
                context.SaveChanges();
            }
        }

        public void DeleteHistory(History history)
        {
            using (var context = new SovaContext())
            {
                context.Histories.Remove(history);
                context.SaveChanges();
            }
        }

        public PagedList<Comment> GetComments(ResourceParameters resourceParameters)
        {
            using (var context = new SovaContext())
            {
                var data = context.Comments.OrderBy(x => x.Id);
                return PagedList<Comment>.Create(data, resourceParameters.PageNumber, resourceParameters.PageSize);
            }
        }

        public Comment GetComment(int id)
        {
            using (var context = new SovaContext())
            {
                return context.Comments.Find(id);
            }
        }

        public PagedList<User> GetUsers(ResourceParameters resourceParameters)
        {
            using (var context = new SovaContext())
            {
                var data = context.Users.OrderBy(x => x.Id);
                return PagedList<User>.Create(data, resourceParameters.PageNumber, resourceParameters.PageSize);
            }
        }

        public User GetUser(int id)
        {
            using (var context = new SovaContext())
            {
                return context.Users.Find(id);
            }
        }

        public PagedList<Tag> GetTags(ResourceParameters resourceParameters)
        {
            using (var context = new SovaContext())
            {
                var data = context.Tags.OrderBy(x => x.Id);
                return PagedList<Tag>.Create(data, resourceParameters.PageNumber, resourceParameters.PageSize);
            }
        }

        public Tag GetTag(int id)
        {
            using (var context = new SovaContext())
            {
                return context.Tags.Find(id);
            }
        }

        public PagedList<SovaUser> GetSovaUsers(ResourceParameters resourceParameters)
        {
            using (var context = new SovaContext())
            {
                var data = context.SovaUsers.OrderBy(x => x.Nick);
                return PagedList<SovaUser>.Create(data, resourceParameters.PageNumber, resourceParameters.PageSize);
            }
        }

        public SovaUser GetSovaUser(int id)
        {
            using (var context = new SovaContext())
            {
                return context.SovaUsers.Find(id);
            }
        }

        public void CreateSovaUser(SovaUser sovaUser)
        {
            using (var context = new SovaContext())
            {
                context.SovaUsers.Add(sovaUser);
                context.SaveChanges();
            }
        }

        public void UpdateSovaUser(SovaUser sovaUser)
        {
            using (var context = new SovaContext())
            {
                context.SovaUsers.Update(sovaUser);
                context.SaveChanges();
            }
        }

        public void DeleteSovaUser(SovaUser sovaUser)
        {
            using (var context = new SovaContext())
            {
                context.SovaUsers.Remove(sovaUser);
                context.SaveChanges();
            }
        }

        public int GetSovaUserCount()
        {
            using (var context = new SovaContext())
            {
                return context.SovaUsers.Count();
            }
        }

        public IList<Posttype> GetPosttypes()
        {
            using (var context = new SovaContext())
            {
                return context.Posttypes.ToList();
            }
        }

        public Posttype GetPosttype(int id)
        {
            using (var context = new SovaContext())
            {
                return context.Posttypes.Find(id);
            }
        }

        
    }
}
