﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YAF.Lucene.Net.Support.Threading
{
    /*
     * Licensed to the Apache Software Foundation (ASF) under one or more
     * contributor license agreements.  See the NOTICE file distributed with
     * this work for additional information regarding copyright ownership.
     * The ASF licenses this file to You under the Apache License, Version 2.0
     * (the "License"); you may not use this file except in compliance with
     * the License.  You may obtain a copy of the License at
     *
     *     https://www.apache.org/licenses/LICENSE-2.0
     *
     * Unless required by applicable law or agreed to in writing, software
     * distributed under the License is distributed on an "AS IS" BASIS,
     * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
     * See the License for the specific language governing permissions and
     * limitations under the License.
     */

    internal class TaskSchedulerCompletionService<T>
    {
        private readonly TaskFactory<T> factory;
        private readonly Queue<Task<T>> taskQueue = new Queue<Task<T>>();

        public TaskSchedulerCompletionService(TaskScheduler scheduler)
        {
            this.factory = new TaskFactory<T>(scheduler ?? TaskScheduler.Default);
        }

        public Task<T> Submit(Func<T> task)
        {
            var t = factory.StartNew(task);
            taskQueue.Enqueue(t);
            return t;
        }

        public Task<T> Take()
        {
            return taskQueue.Dequeue();
        }
    }
}