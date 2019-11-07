using GenericSQLiteRep.Models;
using Microsoft.Win32.SafeHandles;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GenericSQLiteRep
{
	public class BaseModel
	{
		[PrimaryKey, AutoIncrement]
		public int id { get; set; }
	}

	public interface IBaseRepository<T> : IDisposable
		where T : BaseModel, new()
	{
		Task<T> Select(int id);
		Task<List<T>> Select();
		Task<int> Count();
		Task<int> Insert(T data);
		Task<int> Insert(IEnumerable<T> data);
		Task<int> Update(T data);
		Task<int> Update(IEnumerable<T> data);
		Task<int> Delete(T data);
		Task<int> Delete();
	}

	public class Repository<T> : IBaseRepository<T>
		where T : BaseModel, new()
	{
		private static readonly object locker = new object();
		private SQLiteAsyncConnection _connection;
		private string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.db3");

		bool disposed = false;
		SafeHandle sHandle = new SafeFileHandle(IntPtr.Zero, true);

		public Repository()
		{
			lock (locker)
			{
				_connection = new SQLiteAsyncConnection(_path);

				// Start your models here!
				//
				// ex.:
				_connection.CreateTableAsync<ModelExample>();
			}
		}
		public bool IsTableCreated(string tableName)
		{
			lock (locker)
			{
				var tableInfo = _connection.GetConnection().GetTableInfo(tableName);

				if (tableInfo.Count > 0)
					return true;
				return false;
			}
		}

		public Task<List<T>> Select()
		{
			lock (locker)
				return _connection.Table<T>().ToListAsync();
		}

		public Task<T> Select(int id)
		{
			lock (locker)
				return _connection.Table<T>().Where(i => i.id == id).FirstOrDefaultAsync();
		}

		public Task<int> Count()
		{
			lock (locker)
				return _connection.Table<T>().CountAsync();
		}

		public Task<int> Insert(T data)
		{
			lock (locker)
			{
				if (data.id != 0)
					return _connection.UpdateAsync(data);
				else
					return _connection.InsertAsync(data);
			}
		}

		public Task<int> Insert(IEnumerable<T> data)
		{
			lock (locker)
				return _connection.InsertAllAsync(data);
		}

		public Task<int> Update(T data)
		{
			lock (locker)
				return _connection.UpdateAsync(data);
		}

		public Task<int> Update(IEnumerable<T> data)
		{
			lock (locker)
				return _connection.UpdateAllAsync(data);
		}

		public Task<int> Delete(T data)
		{
			lock (locker)
				return _connection.DeleteAsync(data);
		}

		public Task<int> Delete()
		{
			lock (locker)
				return _connection.DeleteAllAsync<T>();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			lock (locker)
			{
				if (disposed)
					return;
				if (disposing)
				{
					sHandle.Dispose();

				}
				_connection = null;
				_path = null;
				disposed = true;
			}
		}

	}
}
