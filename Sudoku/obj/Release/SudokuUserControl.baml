   M S B A M L   `   `   `  ���� A  =Sudoku, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:SudokuSudoku      Sudoku.SudokuUserControl   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     &localclr-namespace:Sudoku     .��]� ����   �� 
StartPoint$  1.5,1.5&� �� EndPoint$	 0,0&�.���  � Color$ #333333�� � Offset$	 0.5Q�� $ #444444��$	 0.4Q�� $ #555555��$	 0.8Q�.+�o� $	��Top=�$��5q�.@�m� $
��File��.@�m� -    m�Name$ mnuNew��$	��New��m� -   $ mnuLoad��$
��Load��m� -   $ mnuLoadFromFile��$��Load From File��m� -   $ mnuSave��$
��Save��m� -   $ 	mnuSaveAs��$��Save As��{�.  m� $
��Edit��.@�m� -   $ mnuUndo��$
��Undo��{�.  m� -   $ mnuRedo��$
��Redo��{�.  m� -   $ mnuSolve��$��Solve��{�.  m� -	   $ mnuCheckProgress��$��Check Progress��]� $��Right=�$��10q�$��Center=�$	��Top=�$
��Auto��.+��� $	��Top=�$��SUDOKU��$��70(�$��Black"�$��Italic$�$��Times New Roman)�$��Center=�
��������� -
    ��Name$ txtTime��$	��Top=�$��	0 50 0 20q�$��40(�$��Center=�$��Impact)�
��������� -   $ txtCurrentDifficulty��$	��Top=�$��40(�$��Center=�$��Impact)��� $	��Top=�$��Current saves:��$��Impact)�
�������$��20(�$��Center=��� -    ��Name$ listCurrentSaves��$	��Top=�$
��Auto=�$ �� IsSynchronizedWithCurrentItem$
 TrueY�$��Stretch=����� ListItemUnsolved��.R��� $��2q�
��� � �
����ح� �� CornerRadius$ 10��._�� .+��� $��5q�$
��Left=����� 	 �� Path$	 Item1�
 �� UpdateSourceTrigger$
 PropertyChanged=��� $��5q�$��Right=����� $	 Item2.CurrentTime�$
 PropertyChanged=��� 
�������$��2q�$��10q�
�������._�� -    �Name$ 	grdSudoku��$��Right=�$��Stretch=�$��Stretch=� � 	Resources    minigridBorderStyle �� 
TargetType �� Property �� Value��%n   &	        �� " ��.���  � ��$ 3����  � ��$ 3����  � ��
 �   ���  � ��$ 2q� � ColumnDefinitions �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* � � RowDefinitions �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* �.+��� $��0��$��0��#��[  �� $��3��$��0��#��[  �� $��6��$��0��#��[  �� $��0��$��3��#��[  �� $��3��$��3��#��[  �� $��6��$��3��#��[  �� $��0��$��6��#��[  �� $��3��$��6��#��[  �� $��6��$��6��#��[  