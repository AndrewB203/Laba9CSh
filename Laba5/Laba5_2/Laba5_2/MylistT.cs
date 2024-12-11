using System;
using System.Collections;
using System.Collections.Generic;

public class MyList<T> : IEnumerable<T>
{
    private T[] _items;
    private int _size;

    // Конструктор по умолчанию
    public MyList()
    {
        _items = new T[4]; // Начальный размер массива
        _size = 0;
    }

    // Свойство для получения количества элементов
    public int Count => _size;

    // Индексатор для получения и установки элемента по индексу
    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= _size)
                throw new IndexOutOfRangeException();
            return _items[index];
        }
        set
        {
            if (index < 0 || index >= _size)
                throw new IndexOutOfRangeException();
            _items[index] = value;
        }
    }

    // Метод для добавления элемента в конец списка
    public void Add(T item)
    {
        if (_size == _items.Length)
        {
            // Увеличиваем размер массива, если он заполнен
            Array.Resize(ref _items, _items.Length * 2);
        }
        _items[_size] = item;
        _size++;
    }

    // Реализация интерфейса IEnumerable<T>
    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < _size; i++)
        {
            yield return _items[i];
        }
    }

    // Явная реализация интерфейса IEnumerable
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
