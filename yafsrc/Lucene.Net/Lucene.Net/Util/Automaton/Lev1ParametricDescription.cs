using YAF.Lucene.Net.Diagnostics;

namespace YAF.Lucene.Net.Util.Automaton
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

    // The following code was generated with the moman/finenight pkg
    // this package is available under the MIT License, see NOTICE.txt
    // for more details.

    using ParametricDescription = YAF.Lucene.Net.Util.Automaton.LevenshteinAutomata.ParametricDescription;

    /// <summary>
    /// Parametric description for generating a Levenshtein automaton of degree 1 </summary>
    internal class Lev1ParametricDescription : ParametricDescription
    {
        override internal int Transition(int absState, int position, int vector)
        {
            // null absState should never be passed in
            if (Debugging.AssertsEnabled) Debugging.Assert(absState != -1);

            // decode absState -> state, offset
            int state = absState / (m_w + 1);
            int offset = absState % (m_w + 1);
            if (Debugging.AssertsEnabled) Debugging.Assert(offset >= 0);

            if (position == m_w)
            {
                if (state < 2)
                {
                    int loc = vector * 2 + state;
                    offset += Unpack(offsetIncrs0, loc, 1);
                    state = Unpack(toStates0, loc, 2) - 1;
                }
            }
            else if (position == m_w - 1)
            {
                if (state < 3)
                {
                    int loc = vector * 3 + state;
                    offset += Unpack(offsetIncrs1, loc, 1);
                    state = Unpack(toStates1, loc, 2) - 1;
                }
            }
            else if (position == m_w - 2)
            {
                if (state < 5)
                {
                    int loc = vector * 5 + state;
                    offset += Unpack(offsetIncrs2, loc, 2);
                    state = Unpack(toStates2, loc, 3) - 1;
                }
            }
            else
            {
                if (state < 5)
                {
                    int loc = vector * 5 + state;
                    offset += Unpack(offsetIncrs3, loc, 2);
                    state = Unpack(toStates3, loc, 3) - 1;
                }
            }

            if (state == -1)
            {
                // null state
                return -1;
            }
            else
            {
                // translate back to abs
                return state * (m_w + 1) + offset;
            }
        }

        // 1 vectors; 2 states per vector; array length = 2
        private readonly static long[] toStates0 = new long[] { 0x2L }; //2 bits per value

        private readonly static long[] offsetIncrs0 = new long[] { 0x0L }; //1 bits per value

        // 2 vectors; 3 states per vector; array length = 6
        private readonly static long[] toStates1 = new long[] { 0xa43L }; //2 bits per value

        private readonly static long[] offsetIncrs1 = new long[] { 0x38L }; //1 bits per value

        // 4 vectors; 5 states per vector; array length = 20
        private readonly static long[] toStates2 = new long[] { 0x69a292450428003L }; //3 bits per value

        private readonly static long[] offsetIncrs2 = new long[] { 0x5555588000L }; //2 bits per value

        // 8 vectors; 5 states per vector; array length = 40
        private readonly static long[] toStates3 = new long[] { 0x1690a82152018003L, 0xb1a2d346448a49L }; //3 bits per value

        private readonly static long[] offsetIncrs3 = new long[] { 0x555555b8220f0000L, 0x5555L }; //2 bits per value

        // state map
        //   0 -> [(0, 0)]
        //   1 -> [(0, 1)]
        //   2 -> [(0, 1), (1, 1)]
        //   3 -> [(0, 1), (2, 1)]
        //   4 -> [(0, 1), (1, 1), (2, 1)]
        public Lev1ParametricDescription(int w)
            : base(w, 1, new int[] { 0, 1, 0, -1, -1 })
        {
        }
    }
}