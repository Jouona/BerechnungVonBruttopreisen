namespace BerechnungVonBruttopreisen.Utilities;

public abstract class SingletonBase<T> where T : class, new() {
    static readonly object lockObject = new();
    static T instance = null;

    protected SingletonBase() { } // Protected constructor to prevent instantiation

    public static T Instance {
        get {
            // double-checked locking für Thread-Sicherheit, in diesem Programm nicht wirklich notwendig
            if (instance == null) {
                lock (lockObject) {
                    if (instance == null) {
                        instance = new T();
                    }
                }
            }

            return instance;
        }
    }
}