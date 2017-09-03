using System;
using System.Collections;
using System.Diagnostics;

namespace test2
{
    class ArrayPerformanceCounter : IDisposable, IEnumerable
    {
        PerformanceCounter[] pcs_;
        public ArrayPerformanceCounter()
        {
            pcs_ = new PerformanceCounter[Environment.ProcessorCount];
        }

        public void Dispose()
        {
            pcs_ = null;
        }

        public int Length { get { return pcs_.Length; } }
        public void setPC(int index, PerformanceCounter pc) {
            pcs_[index] = pc;
        }

        public IEnumerator GetEnumerator()
        {
            for(int i = 0; i < pcs_.Length; i++)
            {
                yield return pcs_[i];
            }
        }
    }
}
