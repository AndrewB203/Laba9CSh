using System;
using System.Collections;
using System.Collections.Generic;

public class MyDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
{
    private struct Entry
    {
        public TKey Key;
        public TValue Value;
    }

    private Entry[] _entries;
    private int _size;

    // Конструктор по умолчанию
    public MyDictionary()
    {
        _entries = new Entry[4]; // Начальный размер массива
        _size = 0;
    }

    // Свойство для получения количества элементов
    public int Count => _size;

    // Индексатор для получения и установки элемента по ключу
    public TValue this[TKey key]
    {
        get
        {
            for (int i = 0; i < _size; i++)
            {
                if (EqualityComparer<TKey>.Default.Equals(_entries[i].Key, key))
                {
                    return _entries[i].Value;
                }
            }
            throw new KeyNotFoundException($"The key '{key}' was not found in the dictionary.");
        }
        set
        {
            for (int i = 0; i < _size; i++)
            {
                if (EqualityComparer<TKey>.Default.Equals(_entries[i].Key, key))
                {
                    _entries[i].Value = value;
                    return;
                }
            }
            Add(key, value);
        }
    }

    // Метод для добавления элемента в словарь
    public void Add(TKey key, TValue value)
    {
        if (_size == _entries.Length)
        {
            // Увеличиваем размер массива, если он заполнен
            Array.Resize(ref _entries, _entries.Length * 2);
        }

        // Проверка на дублирование ключа
        for (int i = 0; i < _size; i++)
        {
            if (EqualityComparer<TKey>.Default.Equals(_entries[i].Key, key))
            {
                throw new ArgumentException($"An item with the same key '{key}' has already been added.");
            }
        }

        _entries[_size].Key = key;
        _entries[_size].Value = value;
        _size++;
    }

    // Реализация интерфейса IEnumerable<KeyValuePair<TKey, TValue>>
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        for (int i = 0; i < _size; i++)
        {
            yield return new KeyValuePair<TKey, TValue>(_entries[i].Key, _entries[i].Value);
        }
    }

    // Явная реализация интерфейса IEnumerable
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}