﻿namespace _02.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;
    using Models;
    using Wintellect.PowerCollections;

    public class Data : IRepository
    {
        private PriorityQueue<int, IEntity> _entities;

        public Data()
        {
            this._entities = new PriorityQueue<int, IEntity>();
        }

        public Data(Data copy)
        {
            this._entities = copy._entities;
        }

        public int Size => this._entities.Count;

        public void Add(IEntity entity)
        {
            this._entities.Enqueue(entity.Id, entity);

            var parentNode = this.GetById((int)entity.ParentId);

            if (parentNode != null)
            {
                parentNode.Children.Add(entity);
            }
        }

        public IRepository Copy()
        {
            Data copy = (Data)this.MemberwiseClone();

            return new Data(copy);
        }

        public IEntity DequeueMostRecent()
        {
            this.EnsureNotEmpty();
            return this._entities.DequeueLast().Value;
        }

        public List<IEntity> GetAll()
        {
            if (this.Size == 0)
            {
                return new List<IEntity>();
            }

            return new List<IEntity>(this._entities.GetAll());
        }

        public List<IEntity> GetAllByType(string type)
        {
            if (type != typeof(Invoice).Name 
                && type != typeof(StoreClient).Name 
                && type != typeof(User).Name)
            {
                throw new InvalidOperationException("Invalid type: " + type);
            }

            var result = this.GetAll().Where(x => x.GetType().Name == type).ToList();
            
            return result;
        }

        public IEntity GetById(int id)
        {
            if (id < 0 || id >= this.Size)
            {
                return null;
            }

            return this._entities.FindByIndex(id);
        }

        public List<IEntity> GetByParentId(int parentId)
        {
            var parentNode = this.GetById(parentId);

            if (parentNode == null)
            {
                return new List<IEntity>();
            }

            return parentNode.Children;
        }

        public IEntity PeekMostRecent()
        {
            this.EnsureNotEmpty();
            return this._entities.PeekLast().Value;
        }

        private void EnsureNotEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Operation on empty Data");
            }
        }
    }
}
