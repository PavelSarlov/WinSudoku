   M S B A M L   `   `   `  ���� A  =Sudoku, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:SudokuSudoku       Sudoku.DifficultyDialogBox   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     &localclr-namespace:Sudoku       � WindowStartupLocation$  CenterScreen=�
������� � Title$ 
Difficulty��$	��200��$	��400��.���� $��Center=�$��Center=� � 	Resources  �� 
TargetType �� Property �� Value��%�   '	�       �� " 	�.���  � ��$ Center=���  � ��$ Center=���  � ��$ 10q���  � ��$ 15(���  � ��$ Impact)�.+��� $��Choose your difficulty:��$��20(�$��Center=�$��Impact)���  �� Orientation$ 
Horizontal=�$��Center=�.+�	� -    	�Name$ rbtnEasy��$
��Easy��
��� d �	� -   $ 
rbtnMedium��$��Medium��
������	� -   $ rbtnHard��$
��Hard��
���  ���� $ 
Horizontal=�$��Center=�  �� 
TargetType	 �� CornerRadius
 �� Triggers P� Property P� Value��%  '��       �� " ��.���  � �� �#373737��  � ��
 �������  � ��$ 20(���  � ��$ Impact)���  �@J�SnapsToDevicePixels . ��  � ��$ 10q���  � ��$ 5q���  � �� �� " ��.R��� $	 4��#��z��._��� #��z��$��Center=�$��Center=�$��5q�
 P�  � {� .  . ���  � ��
 �������  � ��
 �����P�  �@J�IsMouseOver . . ���  � �� �#E59400��  � ��
 �����P�  � �� . . ���  � ��
 � E����  � ��
 �����.+��� -    ��Name$ btnContinue��$��Continue��{�.  �� -   $ 	btnCancel��$��Cancel��