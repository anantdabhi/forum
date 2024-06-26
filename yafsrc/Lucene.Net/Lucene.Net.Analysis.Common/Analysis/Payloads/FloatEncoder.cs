// Lucene version compatibility level 4.8.1
using YAF.Lucene.Net.Util;
using System.Globalization;

namespace YAF.Lucene.Net.Analysis.Payloads
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
    /// Encode a character array <see cref="float"/> as a <see cref="BytesRef"/>.
    /// <para/>
    /// NOTE: This was FloatEncoder in Lucene
    /// </summary>
    /// <seealso cref="PayloadHelper.EncodeSingle(float, byte[], int)"/>
    public class SingleEncoder : AbstractEncoder, IPayloadEncoder
    {
        public override BytesRef Encode(char[] buffer, int offset, int length)
        {
            float payload = float.Parse(new string(buffer, offset, length), CultureInfo.InvariantCulture); //TODO: improve this so that we don't have to new Strings
            byte[] bytes = PayloadHelper.EncodeSingle(payload);
            BytesRef result = new BytesRef(bytes);
            return result;
        }
    }
}