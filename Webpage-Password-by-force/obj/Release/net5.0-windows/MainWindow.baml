   M S B A M L   `   `   `  ���� T  PWebpage-Password-by-force, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Webpage_Password_by_forceWebpage_Password_by_force  *    $Webpage_Password_by_force.MainWindow   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     9local'clr-namespace:Webpage_Password_by_force     -     � Title$  Webpage password by force��$	��700��$
��1200��
���   �.��� $��10,10,10,10q����	#FF232A37 � ColumnDefinitions �� �� $��0* �.+��� -   ���	#FF232A37���	#FF232A37$y�Visible=� � 	Resources  �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius ��Name	 �� Triggers
 P� Property P� Value �� 
TargetName��%`  '��       �� " ��.���  � �� �� " ��.R��� $��Border��$��1,1,1,0q�
�������$ 4,4,0,0��$	��2,0q�._��� $ ContentSite��$��Center=�$��Center=�$��Header��$��10,10q�	 P� 
 �@��
IsSelected$
 True��. ��� $ Border�� � ��
 ��·�P� 
 �@��
IsSelected$ False��. ��� $ Border�� � ��
 �����.@��� $��Attack Control��
����������	#FF61952E$��Normal"�$��Normal$�$��0,0,0,0q� J� RenderTransformOrigin$ 0.5,0.5&�$��	18,8,15,8q�z�V� .~��� �� �� T�  T� X$ 0Q�.��� .+��� -    ��Name$ _scroll3��$
��Auto=�$
��Auto=�$
��True��.��� -    �Name$ attackControls�����	#FF232A37$
��1141��$	��590��.+��� -    ��Name$ percentComplete��$�� ��$
��Left=�$��430,177,0,0q�$	��Top=�$��75��$��26��$��1��
�������$
��Bold"��� -    ��Name$ btnStart��$��	Run Brute��$
��Left=�$��	82,70,0,0q�$	��Top=�4  .System.Windows.Automation.AutomationProperties   Name$ btnStart��$��1��
����������	#FFC21614$��	10,3,10,3q�$
��Bold"�$��16(��� -   $ totalCombinationsTitle��$��Total combinations:��$
��Left=�$��
82,120,0,0q�$	��Top=�$��Normal"�$��13(�
��� ���$ 0.525,0.734&��� -   $ totalCombinations��$��Total combinations:��$
��Left=�$��203,120,0,0q�$	��Top=�$��Normal"�$��13(�
��� ���$ 0.525,0.734&�$��48���� -	   $ progressTitle��$��Statistics:��$
��Left=�$��
82,144,0,0q�$	��Top=�$��Normal"�$��13(�
��� ����� -
    ��Name$ richControl��$��535,10,26,29q����	#FF0E0E0E���	#FF0AB47C���	#FF232A37$��14(�. �/� .T�J� .*�.*��� $�RichTextBox���� -   $ currentPasswordTitle��$��Trying Login:��$
��Left=�$��257,120,0,0q�$	��Top=�$��Normal"�$��13(�
��� ���$ 0.525,0.734&��� -   $ currentPassword��$��Trying Login:��$
��Left=�$��340,120,0,0q�$	��Top=�$��Normal"�$��13(�
��� ���$ 0.525,0.734&�$	��190���� -   $ timeElapsedTitle��$��Time:��$
��Left=�$��257,144,0,0q�$	��Top=�$��Normal"�$��13(�
��� ���$ 0.525,0.734&��� -   $ timeElapsed��$��Time:��$
��Left=�$��303,144,0,0q�$	��Top=�$��Normal"�$��13(�
��� ���$ 0.525,0.734&�$	��255���� -   $ passwordsRemainingTitle��$��Passwords remaining:��$
��Left=�$��257,196,0,0q�$	��Top=�$��Normal"�$��11(����	#FFE4E4E4�� -   $ passwordsRemaining��$��Passwords remaining:��$
��Left=�$��376,196,0,0q�$	��Top=�$��Normal"�$��11(����	#FFE4E4E4�� -   $ passwordsCheckedTitle��$��Passwords checked :��$
��Left=�$��
82,196,0,0q�$	��Top=�$��Normal"�$��11(����	#FFE4E4E4�� -   $ passwordsChecked��$��Passwords checked :��$
��Left=�$��190,196,0,0q�$	��Top=�$��Normal"�$��11(����	#FFE4E4E4$��66��� -    �Name$ progresBar1��$��
87,184,0,0q�$��1��$
��Left=�$	��338��$ 0.5,0.5&�$	��Top=�$��12��z�V� .~���  �� ScaleY$ -1Q��� �� T� �� -   $ validLoginsTitle��$��Valid logins:��$
��Left=�$��
82,392,0,0q�$	��Top=�$��1�����	#FF277ED9$��	ExtraBold"�$��14(��� -    ��Name$ validLogins��$��82,423,0,29q� �� TextWrapping$ NoWrap=�   HelpText$ Enter passwords��$ successfulLogin�� �� AcceptsReturn$
 True��$ �� HorizontalScrollBarVisibility$
 Auto=�" �� VerticalScrollBarVisibility$
 Auto=�$��1�����	#FF232A37���	#FF03D2FE���	#FF232A37$��2,2,2,2q�$
��Left=�$	��417���� -   $ timeRemaining��$��Time remaining:��$
��Left=�$��256,161,0,0q�$	��Top=�$��Normal"�$��11(����	#FFE4E4E4$	��267���� $��Attack Method��.��� ���	#FF232A37 �� $	��251 ���  � RowDefinitions �� $	��15* ��� $
��604* �.+�� $��2��$��2�� �� $��123.008 ��� .+��� -   $	 lbl��$��Select attack method��$
��Left=�$��	53,58,0,0q�$	��Top=�$��1�����	#FF277ED9$
��Bold"�$��26��$	��129���� $
��Left=�$��	91,84,0,0q�$	��Top=�$	��120��$��21��.@��� $��Brute-force attack���� $��Dictionary attack�� �� 
IsSelected$
 True��� $��1��$��0,11,0,0q� �� $	��36* ��� $
��205* �� $��1��$��0,11,0,0q� �� $��84.528 ��� .+�� -   $ 
Dict_files�����	#FF232A37$��0,5,0,0q�$��1��$y�Hidden=�.+��� -   $ 	lbl_Copy2��$��Passworfd file��$
��Left=�$��	56,10,0,0q�$	��Top=�$��1�����	#FF277ED9$
��Bold"��� $
��Left=�$��	94,36,0,0q�$��Password word list file��$
 Wrap=�$	��Top=�$	��342���� $��Browse��$
��Left=�$��
452,34,0,0q�$	��Top=��� -   $ 	lbl_Copy3��$��Username file��$
��Left=�$��	56,71,0,0q�$	��Top=�$��1�����	#FF277ED9$
��Bold"��� $
��Left=�$��	94,97,0,0q�$��User name word list file��$
 Wrap=�$	��Top=�$	��342���� $��Browse��$
��Left=�$��
452,95,0,0q�$	��Top=��� -   $ 	lbl_Copy4��$��Word list source��$
��Left=�$��56,7,0,0q�$	��Top=�$��1�����	#FF277ED9$
��Bold"�$��26���� $
��Left=�$��	94,33,0,0q�$	��Top=�$	��120��$��21��.@��� $��Input��$
 True���� $
��File��� -   $ Dict_inputs��$��1��$��0,17,0,0q�$��1��.+��� -   $ ___Passwords_��$��
Passwords:��$
��Left=�$��59,4,0,0q�$	��Top=�$��1��$��67�����	#FF277ED9$
��Bold"��� -   $ 	Usernames��$��
Usernames:��$
��Left=�$��	236,4,0,0q�$	��Top=�$��26��$��1��$��71�����	#FF277ED9$
��Bold"��� -   $ usernamesWordlist��$
��Left=�$��247,30,0,10q�$(��"David
Alex
Maria
Anna
Marco
Daniel��$ NoWrap=�$	��140��$ Enter usernames ��$ userList��$
 True��$
 Auto=�$
 Auto=����	#FF036891���	#FF03D2FE���	#FF00ACEF$��2,2,2,2q��� -    $ passwordsWordlist��$
��Left=�$��
69,30,0,10q�$����r7rg94w95gt5y98
123456
123456789
12345
qwerty
password
12345678
111111
123123
1234567890
1234567
qwerty123
000000
1q2w3e
aa12345678
abc123
password1
1234
18
qwertyuiop
123321
Password
11111��$ NoWrap=�$	��140��$ Enter passwords��$ passwordList��$
 True��$
 Auto=�$
 Auto=����	#FF036891���	#FF03D2FE���	#FF00ACEF$��2,2,2,2q��� $��Meta data & Headers��$��Center=�$��37��$��Center=�$
��Auto=�$
��Auto=�.��� ���	#FF232A37$
��Auto=�$
��Auto=�$
��auto��$��Stretch=�$��Stretch=�.+��� -!   $ _scroll5��$
��Auto=�$
��Auto=�$
��True��.��� -"   $ MetaInfo��$
��Auto=�$
��Auto=�$
��True��$	��619��$
��1170�� �� $
��294* ��� $
��293* �.+�� -#   $ vertical��$
��Auto=�$
��Auto=�$
��auto��$��Stretch=�$��Stretch=�.+�� -$   $ 
horizontal��$	��Top=�$
��Auto��$
��Auto=�$
��Auto=� �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� -%   $ _scroll2��$
��Auto=�$
��Auto=�$
��True��$��0,0,0,0q�$	��500��.��� $��0,0,0,0q�$��Disabled=�$��Disabled=�$��Stretch=�$
��auto�� �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+�� -&   $	 uri�����	#FF232A37$��Center=�$��80��$��	34,0,34,0q�.+��� -'   $ lbl_Copy��$��
Target URL��$
��Left=�$��0,0,0,0q�$	��Top=����	#FF277ED9$
��Bold"�$��14(��� -(   $ apiUri��$
��Left=�$��	37,36,0,0q�$N��Hhttp://localhost/api/login.php?username=$$$username&password=$$$password��$
 Wrap=�$	��Top=�$	��457���� -)   $ 	lbl_Copy1��$��2��$��Headers��$
��Left=�$��
34,15,80,0q�$	��Top=����	#FF277ED9$
��Bold"�
���   �$��10,10,10,10q��� -*    ��Name$ apiHeadersList��$��3��$��0,0,0,0q����� .R�� 
���   �$��Center=�$��Center=�$��33��$	��518��.+��� $
��Left=�$��	37,10,0,0q�$
 Wrap=�$	��Top=�$	��133������ *vName+�� $
��Left=�$��
197,10,0,0q�$
 Wrap=�$	��Top=�$	��287�����	#FF0EA350���	#FF050505���	#FF0EA350���� *vValue+�� -+   $��X��$
��Left=�$��	497,9,0,0q�$	��Top=����	#FF0EA350���	#FF050505���	#FF0EA350���� *vId+�� $
��Left=�$��
197,10,0,0q�$
 Wrap=�$	��Top=�$	��287��$y�Hidden=����� *vId+� 
���   �$��4��$��Center=�$��Center=�$��33��$	��518��.+��� -,   $ addHeaderName��$
��Left=�$��	37,10,0,0q�$��
Add header��$
 Wrap=�$	��Top=�$	��133�����	#FF979393
���   ����	#FF3E5B49  � ToolTip$  
Add header���� --   $ addHeaderValue��$
��Left=�$��
197,10,0,0q�$��	Add value��$
 Wrap=�$	��Top=�$	��260�����	#FF979393
���   ����	#FF3E5B49$  	Add value���� -.   $	��Add��$
��Left=�$��	462,9,0,0q�$	��Top=�$�� ��$��46�����	#FF979393
���   ����	#FF3E5B49�� -/   $ 	lbl_Copy5��$��5��$��Cookies��$
��Left=�$��
34,15,80,0q�$	��Top=����	#FF277ED9$
��Bold"�
���   �$��10,10,10,10q��� -0   $ apiCookieList��$��6��$��0,0,0,0q����� .R�� 
���   �$��Center=�$��Center=�$��33��$	��518��.+��� $
��Left=�$��	37,10,0,0q�$
 Wrap=�$	��Top=�$	��133��! �� 
IsReadOnly$
! true������ *vName+�� -1   $
��Left=�$��
117,10,0,0q�$
 Wrap=�$	��Top=�$	��135�����	#FF0EA350���	#FF050505���	#FF0EA350$! false������ *vValue+�� $
��Left=�$��
257,10,0,0q�$
 Wrap=�$	��Top=�$��89�����	#FF979393
���   ����	#FF3E5B49$
! true������ *vPath+�� $
��Left=�$��
343,10,0,0q�$
 Wrap=�$	��Top=�$	��114�����	#FF0EA350���	#FF050505���	#FF0EA350$
! true������ *	vDomain+�� -2   $��X��$
��Left=�$��	497,9,0,0q�$	��Top=����	#FF0EA350���	#FF050505���	#FF0EA350���� *vId+�� $
��Left=�$��
197,10,0,0q�$
 Wrap=�$	��Top=�$	��287��$y�Hidden=����� *vId+� 
���   �$��7��$��Center=�$��Center=�$��33��$	��518��.+��� -3   $ addCookieName��$
��Left=�$��	37,10,0,0q�$��
Add cookie��$
 Wrap=�$	��Top=�$��75�����	#FF979393
���   ����	#FF3E5B49$  
Add cookie���� -4   $ addCookieValue��$
��Left=�$��
117,10,0,0q�$��	Add value��$
 Wrap=�$	��Top=�$	��135�����	#FF979393
���   ����	#FF3E5B49$  	Add value���� -5   $ addCookiePath��$
��Left=�$��
257,10,0,0q�$��/��$
 Wrap=�$	��Top=�$��89�����	#FF979393
���   ����	#FF3E5B49$  Cookie path���� -6   $ addCookieDomain��$
��Left=�$��
351,10,0,0q�$��
Add domain��$
 Wrap=�$	��Top=�$	��106�����	#FF979393
���   ����	#FF3E5B49$  
Add domain���� -7   $	��Add��$
��Left=�$��	462,9,0,0q�$	��Top=�$�� ��$��46�����	#FF979393
���   ����	#FF3E5B49� -8   $ 	uri_Copy4��
���   �$��1��$��Center=�$��Center=�$��86��$	��518��.+��� -9   $ 	lbl_Copy9��$��Method��$
��Left=�$��0,0,0,0q�$	��Top=����	#FF277ED9$
��Bold"��� -:   " ��Name$" 	apiMethod��$
��Left=�$��38,0,0,0q�$��Center=�$	��120��.@��� $
��POST��$
 True���� $	��GET��� $��10,0,0,0q�$
��Auto��$��1��
���   � �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� -;   $ 	lbl_Copy6��$��0��$��Payload��$
��Left=�$��0,0,0,0q�$	��Top=����	#FF80D233$��	ExtraBold"�$��14(���Z� �� -<   $ 	lbl_Copy7��$��1��$
��POST��$
��Left=�$��0,0,0,0q�$	��Top=�
�������$
��Bold"�$��14(��� -=   $ apiPostList��$��2��$��0,0,0,0q����� .R�� 
���   �$��Center=�$��Center=�$��33��$	��518��.+��� ->   $
��Left=�$��	33,10,0,0q�$	  Key��$
 Wrap=�$	��Top=�$	��133��
����������	#FF050505���	#FF6A6A6A���� *vName+�� -?   $
��Left=�$��
174,10,0,0q�$	  Val��$
 Wrap=�$	��Top=�$	��287�����	#FF0EA350���	#FF050505���	#FF0EA350���� *vValue+�� -@   $��X��$
��Left=�$��	497,9,0,0q�$	��Top=����	#FF0EA350���	#FF050505���	#FF0EA350���� *vId+� -A   $ 	uri_Copy2��
���   �$��3��$��Center=�$��Center=�$��33��$	��518��.+��� -B   $ addPostName��$
��Left=�$��	33,10,0,0q�$	��Key��$	  Key��$
 Wrap=�$	��Top=�$	��133��
����������	#FF050505���	#FF6A6A6A�� -C   $ 
addPostVal��$
��Left=�$��
174,10,0,0q�$	��Val��$	  Val��$
 Wrap=�$	��Top=�$	��287�����	#FF0EA350���	#FF050505���	#FF0EA350�� -D   $	��Add��$
��Left=�$��
466,10,0,0q�$	��Top=�$�� ��$��46�����	#FF979393
���   ����	#FF3E5B49� -E   $ 	uri_Copy3��
���   �$��Center=�$	��Top=�$	��270��$	��518��$��4��.+��� -F   $ 	lbl_Copy8��$��REST Payload��$
��Left=�$��19,2,0,0q�$	��Top=�
�������$
��Bold"�$��14(��� -G   $ apiRestData��$
��Left=�$��	19,31,0,0q�$
 Wrap=�$	��Top=�$	��471��$	��220�����	#FF0EA350���	#FF050505���	#FF0EA350�� -H   $ 
lbl_Copy10��$��Contains��$
��Left=�$	��Top=����	#FF80D233$��	ExtraBold"�$��14(�$��	52,30,0,0q�$��5����Z� � 
���   �$��6��$��Center=�$��Center=�$��80��$	��518��.+��� -I   $ apiFirstContains��$
��Left=�$��
105,29,0,0q�$�� ��$   ��$
 Wrap=�$	��Top=�$	��393�����	#FFF3FF00���	#FF050505���	#FFF3FF00�� -J   $ apiSecondContains��$
��Left=�$��
105,52,0,0q�$�� ��$   ��$
 Wrap=�$	��Top=�$	��393�����	#FFF3FF00���	#FF050505���	#FFF3FF00�� $��does not contain��$
��Left=�$��	10,47,0,0q�$	��Top=����	#FF7C6C6C�� $��
NET Client��$��Center=�$��37��$��Center=�.��� ���	#FF25112C.+��� -K   $ _scroll4��$
��Auto=�$
��Auto=�$
��True��.��� -L   $ 	netClient��$	��619��$
��1174�� �� $
��237* ��� $
��382* �.+��� -M   $ lblClientURL��$��98,58,480,0q�$
 Wrap=�$	��Top=����	#FF25112C���	#FF6CC3AF�� $	��GET��$
��Left=�$��
110,32,0,0q�$	��Top=����	#FF6CC3AF$	��124���� $��Do GET��$
��Left=�$��
587,81,0,0q�$	��Top=�
����������	#FF951442���	#FF951442� $��1�� �� $
��579* ��� $
��595* �.+��� -N   $ lblHeadersTitle��$��Request Headers��$
��Left=�$��8,11,0,0q�$	��Top=�$��1��$	��183�����	#FF951442$��	ExtraBold"�$��14(��� -O   $ 
lblHeaders��$��	8,34,10,0q�$	��Top=�$��1�����	#FF951442$��Light"�$	��338��$ txtCookies2��.��� $	��741��$	��867��� $��1�� �� $
��auto ��� $
��Auto ��� $
��auto �.+��� -P   $ lblRequestBodyTitle��$��0��$��Request Body��$
��Left=�$��8,-1,0,0q�$	��Top=�$	��183�����	#FF951442$��	ExtraBold"�$��14(��� -Q   $ 
lblPayload��$��1��$��10,23,10,-42q����	#FF951442$��Light"��� -R   $ 
lblCookies��$��2��$��10,23,10,-42q����	#FF951442$��Light"�$ txtCookies2���� $	��Log��$��Center=�$��37��$��Center=�$��68��.��� ���	#FF232A37.+��� -S   $ 
logReports��$��2,50,0,0q�$ NoWrap=�$ 	reportMsg��$
 True��$
 Auto=�$
 Auto=�$��1�����	#FF036891���	#FF03D2FE���	#FF00ACEF$��2,2,2,2q�# �� 	UndoLimit$# 8���� -T   $ btnClear��$��Clear��$
��Left=�$��	10,10,0,0q�$	��Top=�$ btnClear��$ 0.518,0.503&�$��1��
����������	#FFC21614$��14(�$��9,2,8,2q�$
��Bold"�$��Normal$��� -U   $ btnCopy��$��Copy all��$
��Left=�$��	67,10,0,0q�$	��Top=�$ btnClear��$ 0.518,0.503&�$��1��
����������	#FFC21614$��14(�$��9,2,8,2q�$
��Bold"�$��Normal$��� $��Settings��$��Center=�$��37��$��Center=�$��68��.��� ���	#FF232A37.+��� -V   $ settingsDealy��$
��Left=�$��
213,78,0,0q�$	��200��$
 Wrap=�$	��Top=�$	��116���� $��Sleep Dealy (in ms)��$
��Left=�$��	95,74,0,0q�$	��Top=�
��������� -W   $ btnResetAllSettings��$��	Reset all��$
��Left=�$��
95,165,0,0q�$	��Top=��� -X   $ settingsTimeout��$
��Left=�$��213,104,0,0q�$��200000��$
 Wrap=�$	��Top=�$	��116���� $��Timeout��$
��Left=�$��
95,100,0,0q�$	��Top=�
�������