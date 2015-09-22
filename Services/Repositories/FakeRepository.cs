using Data;
using NavTECH.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repositories
{
    public class FakeRepository<T> : IRepository<T>
        where T : IDbModel
    {
        private List<T> _list;

        public FakeRepository(IEnumerable<T> enumerable)
        {
            _list = enumerable.ToList();
        }

        public T Get(int id)
        {
            return _list.SingleOrDefault(l => l.Id == id);
        }

        public int Create(T dbModel)
        {
            var lastId = _list.Max(l => l.Id) + 1;

            dbModel.Id = lastId;
            _list.Add(dbModel);

            return lastId;
        }

        public void Edit(T dbModel)
        {
            _list.RemoveAll(l => l.Id == dbModel.Id);

            _list.Add(dbModel);
        }

        public void Delete(T dbModel)
        {
            Delete(dbModel.Id);
        }

        public void Delete(int id)
        {
            _list.RemoveAll(l => l.Id == id);
        }

        public IQueryable<T> Queryable
        {
            get
            {
                //QueryableExtension.FetchingProvider = () => new FakeFetchingProvider();

                return _list.AsQueryable();
            }
        }

        public int GetNextSequenceValue(string sequenceName)
        {
            var lastId = _list.Max(l => l.Id);

            return lastId++;
        }

        public int GetNextSequenceValue()
        {
            throw new NotImplementedException();
        }

        public void ResetSequenceValue(int value)
        {
            throw new NotImplementedException();
        }
    }
}
