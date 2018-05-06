using System;

namespace WinFormsApp.Model
{
    [Serializable]
    class SequenceItemDto<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
    }
}
