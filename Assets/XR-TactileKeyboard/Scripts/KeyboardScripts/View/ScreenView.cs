using System.Reflection;
using TMPro;
using UnityEngine;

public class ScreenView : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _inputField;

    /// <summary>
    /// Represents how many steps the caret moves within a cycle.
    /// Positive direction represents the direction of the next character.
    /// Negative direction represents the direction of the previous character.
    /// </summary>
    private int _caretChangePosition = 0;

    protected void Awake()
    {
        if(_inputField == null)
        {
            Debug.LogWarning("Missing text input field to display the text output.", this);
        }

        SetCaretToVisible();
        MoveCaretPosition(_inputField.text.Length);
    }

    /// <summary>
    /// This LateUpdate sets the position of the caret after each render cycle.
    /// This is necessary since caretPosition of inputField updates very late into the render cycle.
    /// So updating the caretPosition of inputField, might not update the property in time for the next
    /// input during the same cycle.
    /// For example inserting a character two times might cause the caret to move only one step instead of two.
    /// Waiting for LateUpdate before updating the position guarantees that the
    /// resulting value of the caretPosition is accurate.
    /// </summary>
    private void LateUpdate()
    {   
        SetCaretPosition(_caretChangePosition + _inputField.caretPosition);
        //resetting the value for the next cycle
        _caretChangePosition = 0;
    }

    public void InsertString(string text)
    {
        _inputField.text = _inputField.text.Insert(_inputField.caretPosition, text);
        
        MoveCaretPosition(text.Length);
    }

    public void RemoveNextCharacter()
    {
        if(_inputField.caretPosition < _inputField.text.Length)
        {
            _inputField.text = _inputField.text.Remove(_inputField.caretPosition, 1);
        }
    }

    public void RemovePreviousCharacter()
    {
        if(_inputField.caretPosition > 0)
        {
            _inputField.text = _inputField.text.Remove(_inputField.caretPosition-1, 1);

            // Removing the last element of a text also automatically adjusts the caret position.    
            // So only adjust caret position if it's not at the end of the text after removal.
            if(_inputField.caretPosition < _inputField.text.Length)
            {
                MoveCaretPosition(-1);
            }
        }
    }

    public void BeginNewLine()
    {
        char lineFeed = (char) 10;
        InsertString(lineFeed.ToString());
    }

    public void MoveCaretToPreviousCharacter()
    {
        MoveCaretPosition(-1);
    }

    public void MoveCaretPosition(int positionChange)
    {
        _caretChangePosition += positionChange;
    }

    public void MoveCaretToNextCharacter()
    {
        MoveCaretPosition(1);
    }

    /// <summary>
    /// Should only be called from within LateUpdate
    /// </summary>
    /// <param name="position"></param>
    private void SetCaretPosition(int position)
    {
        _inputField.caretPosition = position;
    }

    /// <summary>
    /// Using Reflection to make the caret visible even if the input field is not selected
    /// </summary>
    private void SetCaretToVisible()
    {
        _inputField.GetType().GetField("m_AllowInput", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(_inputField, true);
        _inputField.GetType().InvokeMember("SetCaretVisible", BindingFlags.NonPublic | BindingFlags.InvokeMethod | BindingFlags.Instance, null, _inputField, null);
    }
}
