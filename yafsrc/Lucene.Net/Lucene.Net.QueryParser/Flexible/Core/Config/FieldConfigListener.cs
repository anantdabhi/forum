﻿namespace YAF.Lucene.Net.QueryParsers.Flexible.Core.Config
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

    /// <summary>
    /// This interface should be implemented by classes that wants to listen for
    /// field configuration requests. The implementation receives a
    /// <see cref="FieldConfig"/> object and may add/change its configuration.
    /// </summary>
    /// <seealso cref="FieldConfig"/>
    /// <seealso cref="QueryConfigHandler"/>
    public interface IFieldConfigListener
    {
        /// <summary>
        /// This method is called every time a field configuration is requested.
        /// </summary>
        /// <param name="fieldConfig">the field configuration requested, should never be null</param>
        void BuildFieldConfig(FieldConfig fieldConfig);
    }
}
