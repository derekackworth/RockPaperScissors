   M S B A M L   `   `   `  ����   	RPSClient    RPSClient.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     � Title$  Rock Paper Scissors��$	��450��$	��600�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � 	Resources  �� Source�� $ RPSClientDictionary.xaml?�%    .���  � RowDefinitions �� $��2* ��� .+�� $��0�� � ColumnDefinitions �� �� .+��� $��0��
���   �$��0 0 0 1q��� -   $��
txtMessage��$��0��$��2��$��Center=�$	��Top=�$��18(�$
��Bold"��� -   $��
txtPlayer1��$��0��$��0, 235, 0, 0q�$��Center=�$��18(��� -   $��txtPlayer1Wins��$��0��$��0, 257, 0, 0q�$��Center=�$��10(��� -   $��
imgPlayer1��$��0��$��Center=�$��Center=�$	��150���� $��1��
���   �$��0 0 0 1q��� -   $��
txtPlayer2��$��1��$��0, 235, 0, 0q�$��Center=�$��18(��� -   $��
imgPlayer2��$��1��$��Center=�$��Center=�$	��150�� J� RenderTransformOrigin$ 0.5, 0.5&�z���  �� ScaleX$ -1Q�� $��1�� �� $��4* ��� $��3* ��� $��4* � �� �� �� .+��� -   $��txtQueue��$��0��$��0��$��Center=�$	��Top=�$��Queue��$y�Hidden=��� -	   $��lstQueue��$��0��$��0��$��3��$��Center=�$y�Hidden=��� $��1��$��0��$��3��
���   �$��1 0 1 0q��� -
   $��btnRock��$��1��$��0��$
��Rock��{�.  �� -   $��btnPaper��$��1��$��1��$��Paper��{�.  �� -   $��btnScissors��$��1��$��2��$��Scissors��{�.  �� -   $��txtUsername��$��2��$��2��$��48.4, 1.2, -48.4, 0q�$	��Top=�$��
Username: ���� -   $��
tbUsername��$��2��$��2��$��Right=�$	��Top=�$	��100���� -   $��btnLoginLogout��$��2��$��2��$��Right=�$��Bottom=�$	��100��$��Login��