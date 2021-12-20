using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//представление квадрата в сетке крестики-нолики
public class Square
{
    private Button button; // GUI Panel для Square
    private char mark; // метка игрока на этом Square
    private int location; // расположение на доске этого Square

    // конструктор
    public Square(Button newPanel, char newMark, int newLocation)
    {
        button = newPanel;
        mark = newMark;
        location = newLocation;
    }

    public Button SquareButton
    {
        get
        {
            return button;
        } 
    } 

    public char Mark
    {
        get
        {
            return mark;
        } 
        set
        {
            mark = value;
        } 
    } 


    public int Location
    {
        get
        {
            return location;
        }
        set
        {
            location = value;
        }
    }  
} 
