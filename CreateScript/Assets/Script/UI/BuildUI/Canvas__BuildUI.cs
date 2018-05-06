using UnityEngine;
using UnityEngine.UI;
using System;

public partial class Canvas__UI :MonoBehaviour
{
	[SerializeField] private InputField           InputField_InputField;
	[SerializeField] private Text                 Text_Placeholder;
	[SerializeField] private Text                 Text_Text;
	[SerializeField] private Button               Button_Button;
	[SerializeField] private Text                 Text_Text1;
	[SerializeField] private Toggle               Toggle_Toggle;
	[SerializeField] private Image                Image_Background;
	[SerializeField] private Image                Image_Checkmark;
	[SerializeField] private Text                 Text_Label;
	[SerializeField] private Slider               Slider_Slider;
	[SerializeField] private Image                Image_Background1;
	[SerializeField] private Transform            Transform_Fill_Area;
	[SerializeField] private Image                Image_Fill;
	[SerializeField] private Transform            Transform_Handle_Slide_Area;
	[SerializeField] private Image                Image_Handle;
	[SerializeField] private Scrollbar            Scrollbar_Scrollbar;
	[SerializeField] private Transform            Transform_Sliding_Area;
	[SerializeField] private Image                Image_Handle1;
	[SerializeField] private Dropdown             Dropdown_Dropdown;
	[SerializeField] private Text                 Text_Label1;
	[SerializeField] private Image                Image_Arrow;
	[SerializeField] private ScrollRect           ScrollRect_Template;
	[SerializeField] private Image                Image_Viewport;
	[SerializeField] private Transform            Transform_Content;
	[SerializeField] private Toggle               Toggle_Item;
	[SerializeField] private Image                Image_Item_Background;
	[SerializeField] private Image                Image_Item_Checkmark;
	[SerializeField] private Text                 Text_Item_Label;
	[SerializeField] private Scrollbar            Scrollbar_Scrollbar1;
	[SerializeField] private Transform            Transform_Sliding_Area1;
	[SerializeField] private Image                Image_Handle2;
	[SerializeField] private ScrollRect           ScrollRect_Scroll_View;
	[SerializeField] private Image                Image_Viewport1;
	[SerializeField] private Transform            Transform_Content1;
	[SerializeField] private Scrollbar            Scrollbar_Scrollbar_Horizontal;
	[SerializeField] private Transform            Transform_Sliding_Area2;
	[SerializeField] private Image                Image_Handle3;
	[SerializeField] private Scrollbar            Scrollbar_Scrollbar_Vertical;
	[SerializeField] private Transform            Transform_Sliding_Area3;
	[SerializeField] private Image                Image_Handle4;
	[SerializeField] private Text                 Text_Text2;
	[SerializeField] private Image                Image_Image;
	[SerializeField] private RawImage             RawImage_RawImage;

	private QInputField qInputField_InputField;
	private QText qText_Placeholder;
	private QText qText_Text;
	private QButton qButton_Button;
	private QText qText_Text1;
	private QToggle qToggle_Toggle;
	private QImage qImage_Background;
	private QImage qImage_Checkmark;
	private QText qText_Label;
	private QSlider qSlider_Slider;
	private QImage qImage_Background1;
	private QImage qImage_Fill;
	private QImage qImage_Handle;
	private QScrollbar qScrollbar_Scrollbar;
	private QImage qImage_Handle1;
	private QDropdown qDropdown_Dropdown;
	private QText qText_Label1;
	private QImage qImage_Arrow;
	private QScrollRect qScrollRect_Template;
	private QImage qImage_Viewport;
	private QToggle qToggle_Item;
	private QImage qImage_Item_Background;
	private QImage qImage_Item_Checkmark;
	private QText qText_Item_Label;
	private QScrollbar qScrollbar_Scrollbar1;
	private QImage qImage_Handle2;
	private QScrollRect qScrollRect_Scroll_View;
	private QImage qImage_Viewport1;
	private QScrollbar qScrollbar_Scrollbar_Horizontal;
	private QImage qImage_Handle3;
	private QScrollbar qScrollbar_Scrollbar_Vertical;
	private QImage qImage_Handle4;
	private QText qText_Text2;
	private QImage qImage_Image;
	private QRawImage qRawImage_RawImage;

	public Action<string>             onInputField_InputFieldEndEdit;
	public Action                     onButton_ButtonClick;
	public Action<bool>               onToggle_ToggleValueChanged;
	public Action<float>              onSlider_SliderValueChanged;
	public Action<float>              onScrollbar_ScrollbarValueChanged;
	public Action<int>                onDropdown_DropdownValueChanged;
	public Action<Vector2>            onScrollRect_TemplateValueChanged;
	public Action<bool>               onToggle_ItemValueChanged;
	public Action<float>              onScrollbar_Scrollbar1ValueChanged;
	public Action<Vector2>            onScrollRect_Scroll_ViewValueChanged;
	public Action<float>              onScrollbar_Scrollbar_HorizontalValueChanged;
	public Action<float>              onScrollbar_Scrollbar_VerticalValueChanged;

	public QInputField QInputField_InputField{get{return qInputField_InputField;}}
	public QText QText_Placeholder{get{return qText_Placeholder;}}
	public QText QText_Text{get{return qText_Text;}}
	public QButton QButton_Button{get{return qButton_Button;}}
	public QText QText_Text1{get{return qText_Text1;}}
	public QToggle QToggle_Toggle{get{return qToggle_Toggle;}}
	public QImage QImage_Background{get{return qImage_Background;}}
	public QImage QImage_Checkmark{get{return qImage_Checkmark;}}
	public QText QText_Label{get{return qText_Label;}}
	public QSlider QSlider_Slider{get{return qSlider_Slider;}}
	public QImage QImage_Background1{get{return qImage_Background1;}}
	public Transform QTransform_Fill_Area{get{return Transform_Fill_Area;}}
	public QImage QImage_Fill{get{return qImage_Fill;}}
	public Transform QTransform_Handle_Slide_Area{get{return Transform_Handle_Slide_Area;}}
	public QImage QImage_Handle{get{return qImage_Handle;}}
	public QScrollbar QScrollbar_Scrollbar{get{return qScrollbar_Scrollbar;}}
	public Transform QTransform_Sliding_Area{get{return Transform_Sliding_Area;}}
	public QImage QImage_Handle1{get{return qImage_Handle1;}}
	public QDropdown QDropdown_Dropdown{get{return qDropdown_Dropdown;}}
	public QText QText_Label1{get{return qText_Label1;}}
	public QImage QImage_Arrow{get{return qImage_Arrow;}}
	public QScrollRect QScrollRect_Template{get{return qScrollRect_Template;}}
	public QImage QImage_Viewport{get{return qImage_Viewport;}}
	public Transform QTransform_Content{get{return Transform_Content;}}
	public QToggle QToggle_Item{get{return qToggle_Item;}}
	public QImage QImage_Item_Background{get{return qImage_Item_Background;}}
	public QImage QImage_Item_Checkmark{get{return qImage_Item_Checkmark;}}
	public QText QText_Item_Label{get{return qText_Item_Label;}}
	public QScrollbar QScrollbar_Scrollbar1{get{return qScrollbar_Scrollbar1;}}
	public Transform QTransform_Sliding_Area1{get{return Transform_Sliding_Area1;}}
	public QImage QImage_Handle2{get{return qImage_Handle2;}}
	public QScrollRect QScrollRect_Scroll_View{get{return qScrollRect_Scroll_View;}}
	public QImage QImage_Viewport1{get{return qImage_Viewport1;}}
	public Transform QTransform_Content1{get{return Transform_Content1;}}
	public QScrollbar QScrollbar_Scrollbar_Horizontal{get{return qScrollbar_Scrollbar_Horizontal;}}
	public Transform QTransform_Sliding_Area2{get{return Transform_Sliding_Area2;}}
	public QImage QImage_Handle3{get{return qImage_Handle3;}}
	public QScrollbar QScrollbar_Scrollbar_Vertical{get{return qScrollbar_Scrollbar_Vertical;}}
	public Transform QTransform_Sliding_Area3{get{return Transform_Sliding_Area3;}}
	public QImage QImage_Handle4{get{return qImage_Handle4;}}
	public QText QText_Text2{get{return qText_Text2;}}
	public QImage QImage_Image{get{return qImage_Image;}}
	public QRawImage QRawImage_RawImage{get{return qRawImage_RawImage;}}

	partial void OnAwake();
	private void Awake()
	{
		OnAwake();

		InputField_InputField     = transform.Find("InputField").GetComponent<InputField>();
		Text_Placeholder          = transform.Find("InputField/Placeholder").GetComponent<Text>();
		Text_Text                 = transform.Find("InputField/Text").GetComponent<Text>();
		Button_Button             = transform.Find("Button").GetComponent<Button>();
		Text_Text1                = transform.Find("Button/Text").GetComponent<Text>();
		Toggle_Toggle             = transform.Find("Toggle").GetComponent<Toggle>();
		Image_Background          = transform.Find("Toggle/Background").GetComponent<Image>();
		Image_Checkmark           = transform.Find("Toggle/Background/Checkmark").GetComponent<Image>();
		Text_Label                = transform.Find("Toggle/Label").GetComponent<Text>();
		Slider_Slider             = transform.Find("Slider").GetComponent<Slider>();
		Image_Background1         = transform.Find("Slider/Background").GetComponent<Image>();
		Transform_Fill_Area       = transform.Find("Slider/Fill Area").GetComponent<Transform>();
		Image_Fill                = transform.Find("Slider/Fill Area/Fill").GetComponent<Image>();
		Transform_Handle_Slide_Area = transform.Find("Slider/Handle Slide Area").GetComponent<Transform>();
		Image_Handle              = transform.Find("Slider/Handle Slide Area/Handle").GetComponent<Image>();
		Scrollbar_Scrollbar       = transform.Find("Scrollbar").GetComponent<Scrollbar>();
		Transform_Sliding_Area    = transform.Find("Scrollbar/Sliding Area").GetComponent<Transform>();
		Image_Handle1             = transform.Find("Scrollbar/Sliding Area/Handle").GetComponent<Image>();
		Dropdown_Dropdown         = transform.Find("Dropdown").GetComponent<Dropdown>();
		Text_Label1               = transform.Find("Dropdown/Label").GetComponent<Text>();
		Image_Arrow               = transform.Find("Dropdown/Arrow").GetComponent<Image>();
		ScrollRect_Template       = transform.Find("Dropdown/Template").GetComponent<ScrollRect>();
		Image_Viewport            = transform.Find("Dropdown/Template/Viewport").GetComponent<Image>();
		Transform_Content         = transform.Find("Dropdown/Template/Viewport/Content").GetComponent<Transform>();
		Toggle_Item               = transform.Find("Dropdown/Template/Viewport/Content/Item").GetComponent<Toggle>();
		Image_Item_Background     = transform.Find("Dropdown/Template/Viewport/Content/Item/Item Background").GetComponent<Image>();
		Image_Item_Checkmark      = transform.Find("Dropdown/Template/Viewport/Content/Item/Item Checkmark").GetComponent<Image>();
		Text_Item_Label           = transform.Find("Dropdown/Template/Viewport/Content/Item/Item Label").GetComponent<Text>();
		Scrollbar_Scrollbar1      = transform.Find("Dropdown/Template/Scrollbar").GetComponent<Scrollbar>();
		Transform_Sliding_Area1   = transform.Find("Dropdown/Template/Scrollbar/Sliding Area").GetComponent<Transform>();
		Image_Handle2             = transform.Find("Dropdown/Template/Scrollbar/Sliding Area/Handle").GetComponent<Image>();
		ScrollRect_Scroll_View    = transform.Find("Scroll View").GetComponent<ScrollRect>();
		Image_Viewport1           = transform.Find("Scroll View/Viewport").GetComponent<Image>();
		Transform_Content1        = transform.Find("Scroll View/Viewport/Content").GetComponent<Transform>();
		Scrollbar_Scrollbar_Horizontal = transform.Find("Scroll View/Scrollbar Horizontal").GetComponent<Scrollbar>();
		Transform_Sliding_Area2   = transform.Find("Scroll View/Scrollbar Horizontal/Sliding Area").GetComponent<Transform>();
		Image_Handle3             = transform.Find("Scroll View/Scrollbar Horizontal/Sliding Area/Handle").GetComponent<Image>();
		Scrollbar_Scrollbar_Vertical = transform.Find("Scroll View/Scrollbar Vertical").GetComponent<Scrollbar>();
		Transform_Sliding_Area3   = transform.Find("Scroll View/Scrollbar Vertical/Sliding Area").GetComponent<Transform>();
		Image_Handle4             = transform.Find("Scroll View/Scrollbar Vertical/Sliding Area/Handle").GetComponent<Image>();
		Text_Text2                = transform.Find("Text").GetComponent<Text>();
		Image_Image               = transform.Find("Image").GetComponent<Image>();
		RawImage_RawImage         = transform.Find("RawImage").GetComponent<RawImage>();

		qInputField_InputField                             = new QInputField(InputField_InputField);
		qText_Placeholder                                  = new QText(Text_Placeholder);
		qText_Text                                         = new QText(Text_Text);
		qButton_Button                                     = new QButton(Button_Button);
		qText_Text1                                        = new QText(Text_Text1);
		qToggle_Toggle                                     = new QToggle(Toggle_Toggle);
		qImage_Background                                  = new QImage(Image_Background);
		qImage_Checkmark                                   = new QImage(Image_Checkmark);
		qText_Label                                        = new QText(Text_Label);
		qSlider_Slider                                     = new QSlider(Slider_Slider);
		qImage_Background1                                 = new QImage(Image_Background1);
		qImage_Fill                                        = new QImage(Image_Fill);
		qImage_Handle                                      = new QImage(Image_Handle);
		qScrollbar_Scrollbar                               = new QScrollbar(Scrollbar_Scrollbar);
		qImage_Handle1                                     = new QImage(Image_Handle1);
		qDropdown_Dropdown                                 = new QDropdown(Dropdown_Dropdown);
		qText_Label1                                       = new QText(Text_Label1);
		qImage_Arrow                                       = new QImage(Image_Arrow);
		qScrollRect_Template                               = new QScrollRect(ScrollRect_Template);
		qImage_Viewport                                    = new QImage(Image_Viewport);
		qToggle_Item                                       = new QToggle(Toggle_Item);
		qImage_Item_Background                             = new QImage(Image_Item_Background);
		qImage_Item_Checkmark                              = new QImage(Image_Item_Checkmark);
		qText_Item_Label                                   = new QText(Text_Item_Label);
		qScrollbar_Scrollbar1                              = new QScrollbar(Scrollbar_Scrollbar1);
		qImage_Handle2                                     = new QImage(Image_Handle2);
		qScrollRect_Scroll_View                            = new QScrollRect(ScrollRect_Scroll_View);
		qImage_Viewport1                                   = new QImage(Image_Viewport1);
		qScrollbar_Scrollbar_Horizontal                    = new QScrollbar(Scrollbar_Scrollbar_Horizontal);
		qImage_Handle3                                     = new QImage(Image_Handle3);
		qScrollbar_Scrollbar_Vertical                      = new QScrollbar(Scrollbar_Scrollbar_Vertical);
		qImage_Handle4                                     = new QImage(Image_Handle4);
		qText_Text2                                        = new QText(Text_Text2);
		qImage_Image                                       = new QImage(Image_Image);
		qRawImage_RawImage                                 = new QRawImage(RawImage_RawImage);

		InputField_InputField.onEndEdit.AddListener( OnInputField_InputFieldEndEdit );
		Button_Button.onClick.AddListener( OnButton_ButtonClick );
		Toggle_Toggle.onValueChanged.AddListener( OnToggle_ToggleValueChanged );
		Slider_Slider.onValueChanged.AddListener( OnSlider_SliderValueChanged );
		Scrollbar_Scrollbar.onValueChanged.AddListener( OnScrollbar_ScrollbarValueChanged );
		Dropdown_Dropdown.onValueChanged.AddListener( OnDropdown_DropdownValueChanged );
		ScrollRect_Template.onValueChanged.AddListener( OnScrollRect_TemplateValueChanged );
		Toggle_Item.onValueChanged.AddListener( OnToggle_ItemValueChanged );
		Scrollbar_Scrollbar1.onValueChanged.AddListener( OnScrollbar_Scrollbar1ValueChanged );
		ScrollRect_Scroll_View.onValueChanged.AddListener( OnScrollRect_Scroll_ViewValueChanged );
		Scrollbar_Scrollbar_Horizontal.onValueChanged.AddListener( OnScrollbar_Scrollbar_HorizontalValueChanged );
		Scrollbar_Scrollbar_Vertical.onValueChanged.AddListener( OnScrollbar_Scrollbar_VerticalValueChanged );

	}
	private void OnInputField_InputFieldEndEdit(string value)
	{
		if(onInputField_InputFieldEndEdit!=null)onInputField_InputFieldEndEdit(value);
	}
	private void OnButton_ButtonClick()
	{
		if(onButton_ButtonClick!=null)onButton_ButtonClick();
	}
	private void OnToggle_ToggleValueChanged(bool value)
	{
		if(onToggle_ToggleValueChanged!=null)onToggle_ToggleValueChanged(value);
	}
	private void OnSlider_SliderValueChanged(float value)
	{
		if(onSlider_SliderValueChanged!=null)onSlider_SliderValueChanged(value);
	}
	private void OnScrollbar_ScrollbarValueChanged(float value)
	{
		if(onScrollbar_ScrollbarValueChanged!=null)onScrollbar_ScrollbarValueChanged(value);
	}
	private void OnDropdown_DropdownValueChanged(int value)
	{
		if(onDropdown_DropdownValueChanged!=null)onDropdown_DropdownValueChanged(value);
	}
	private void OnScrollRect_TemplateValueChanged(Vector2 value)
	{
		if(onScrollRect_TemplateValueChanged!=null)onScrollRect_TemplateValueChanged(value);
	}
	private void OnToggle_ItemValueChanged(bool value)
	{
		if(onToggle_ItemValueChanged!=null)onToggle_ItemValueChanged(value);
	}
	private void OnScrollbar_Scrollbar1ValueChanged(float value)
	{
		if(onScrollbar_Scrollbar1ValueChanged!=null)onScrollbar_Scrollbar1ValueChanged(value);
	}
	private void OnScrollRect_Scroll_ViewValueChanged(Vector2 value)
	{
		if(onScrollRect_Scroll_ViewValueChanged!=null)onScrollRect_Scroll_ViewValueChanged(value);
	}
	private void OnScrollbar_Scrollbar_HorizontalValueChanged(float value)
	{
		if(onScrollbar_Scrollbar_HorizontalValueChanged!=null)onScrollbar_Scrollbar_HorizontalValueChanged(value);
	}
	private void OnScrollbar_Scrollbar_VerticalValueChanged(float value)
	{
		if(onScrollbar_Scrollbar_VerticalValueChanged!=null)onScrollbar_Scrollbar_VerticalValueChanged(value);
	}

}