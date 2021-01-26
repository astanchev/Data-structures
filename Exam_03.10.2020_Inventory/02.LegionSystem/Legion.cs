namespace _02.LegionSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using Interfaces;
    using Data.Models;
    using Models;
    using Wintellect.PowerCollections;

    public class Legion : IArmy
    {
        private PriorityQueue<int, IEnemy> _enemies;

        public Legion()
        {
            this._enemies = new PriorityQueue<int, IEnemy>();
        }

        public int Size => this._enemies.Count;

        public bool Contains(IEnemy enemy)
        {
            var resultEnemy = this._enemies.FindByPriority(enemy.AttackSpeed);

            return resultEnemy != null;
        }

        public void Create(IEnemy enemy)
        {
            if (!this.Contains(enemy))
            {
                this._enemies.Enqueue(enemy.AttackSpeed, enemy);
            }
        }

        public IEnemy GetByAttackSpeed(int speed)
        {
            var enemy = this._enemies.FindByPriority(speed);

            return enemy == default ? null : enemy;
        }

        public List<IEnemy> GetFaster(int speed)
        {
            var result = new List<IEnemy>(this.Size);

            foreach (var enemy in this._enemies.GetAll())
            {
                if (enemy.AttackSpeed > speed)
                {
                    result.Add(enemy);
                }
            }

            return result;
        }

        public IEnemy GetFastest()
        {
            this.EnsureNotEmpty();

            return this._enemies.PeekLast().Value;
        }

        public IEnemy[] GetOrderedByHealth()
        {
            if (this._enemies.Count == 0)
            {
                return new IEnemy[0];
            }

            var byHealthSet = new SortedDictionary<int, List<IEnemy>>(new HealthComparer());
            foreach (var enemy in _enemies)
            {
                if (!byHealthSet.ContainsKey(enemy.Health))
                {
                    byHealthSet[enemy.Health] = new List<IEnemy>();
                }
                byHealthSet[enemy.Health].Add(enemy);
            }

            return byHealthSet.SelectMany(x => x.Value).ToArray();
        }

        // Load performance issue
        //public IEnemy[] GetOrderedByHealth()
        //{
        //    if (this._enemies.Count == 0)
        //    {
        //        return new IEnemy[0];
        //    }

        //    return this._enemies
        //        .GetAll()
        //        .OrderByDescending(e => e.Health)
        //        .ToArray();
        //}

        public List<IEnemy> GetSlower(int speed)
        {
            var result = new List<IEnemy>(this.Size);

            foreach (var enemy in this._enemies.GetAll())
            {
                if (enemy.AttackSpeed < speed)
                {
                    result.Add(enemy);
                }
            }

            return result;
        }

        public IEnemy GetSlowest()
        {
            this.EnsureNotEmpty();

            return this._enemies.Peek().Value;
        }

        public void ShootFastest()
        {
            this.EnsureNotEmpty();

            this._enemies.DequeueLast();
        }

        public void ShootSlowest()
        {
            this.EnsureNotEmpty();

            this._enemies.Dequeue();
        }

        private void EnsureNotEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Legion has no enemies!");
            }
        }

        private int CompareByHealth(IEnemy first, IEnemy second)
        {
            return second.Health - first.Health;
        }
    }
}
