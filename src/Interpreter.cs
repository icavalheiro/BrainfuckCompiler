using System;
using System.Collections.Generic;

namespace BFC
{
    public class Interpreter
    {
        private readonly string _program;
        private readonly Action<char> _outputFunction;
        private readonly Func<int> _inputFunction;
        private int _pointer;
        private List<int> _memory;

        public Interpreter(string program, Action<char> outputFunction, Func<int> inputFunction)
        {
            _program = program;
            _outputFunction = outputFunction;
            _inputFunction = inputFunction;
        }

        private void Reset()
        {
            _pointer = 0;
            _memory = new List<int> { 0 };
        }

        private void IncrementPointer()
        {
            _pointer++;
            if(_pointer >= _memory.Count)
                _memory.Add(0);
        }

        private void DecrementPointer()
        {
            _pointer--;
            if (_pointer < 0)
            {
                _pointer = 0;
            }
        }

        private void IncrementCurrentPosition()
        {
            _memory[_pointer]++;
        }

        private void DecrementCurrentPosition()
        {
            _memory[_pointer]--;
        }

        private void OutputCurrentPosition()
        {
            _outputFunction?.Invoke((char)_memory[_pointer]);
        }
        
        private void InputAtCurrentPosition()
        {
            if(_inputFunction != null)
                _memory[_pointer] = (int) _inputFunction();
        }

        private bool IsCurrentPositionZero()
        {
            return _memory[_pointer] == 0;
        }
        
        public void Run()
        {
            Reset();
            InterpretScope(0);
        }
        
        private int InterpretScope(int index)
        {
            var startingIndex = index;
            while (index < _program.Length)
            {
                var currentCommand = _program[index];
                switch (currentCommand)
                {
                    default://unknown command character
                        break;
                    
                    case '>':
                        IncrementPointer();
                        break;
                    
                    case '<':
                        DecrementPointer();
                        break;
                    
                    case '+':
                        IncrementCurrentPosition();
                        break;
                    
                    case '-':
                        DecrementCurrentPosition();
                        break;
                    
                    case '.':
                        OutputCurrentPosition();
                        break;
                    
                    case ',':
                        InputAtCurrentPosition();
                        break;
                    
                    case '[':
                        index = InterpretScope(index + 1);
                        break;
                    
                    case ']':
                        if (IsCurrentPositionZero())
                            return index;

                        index = startingIndex-1;
                        break;
                }

                index++;
            }

            return _program.Length;
        }
    }
}