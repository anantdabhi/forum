﻿using YAF.Lucene.Net.Util;
using System;

namespace YAF.Lucene.Net.Codecs
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
    /// When placed on a class that subclasses <see cref="PostingsFormat"/>, adding this
    /// attribute will exclude the type from consideration in the 
    /// <see cref="DefaultPostingsFormatFactory.ScanForPostingsFormats(System.Reflection.Assembly)"/> method.
    /// <para/>
    /// However, the <see cref="PostingsFormat"/> type can still be added manually using
    /// <see cref="DefaultPostingsFormatFactory.PutPostingsFormatType(Type)"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class ExcludePostingsFormatFromScanAttribute : ExcludeServiceAttribute
    {
    }
}
