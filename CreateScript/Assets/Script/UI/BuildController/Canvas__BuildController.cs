using UnityEngine;



public partial class Canvas__Controller
{
	private Canvas__UI ui;
	private Canvas__Model model = new Canvas__Model();

	public Canvas__Controller(Canvas__UI ui)
	{
		this.ui = ui;
		OnAwake();
		ui.onInputField_InputFieldEndEdit                     = OnInputField_InputFieldEndEdit;
		ui.onButton_ButtonClick                               = OnButton_ButtonClick;
		ui.onToggle_ToggleValueChanged                        = OnToggle_ToggleValueChanged;
		ui.onSlider_SliderValueChanged                        = OnSlider_SliderValueChanged;
		ui.onScrollbar_ScrollbarValueChanged                  = OnScrollbar_ScrollbarValueChanged;
		ui.onDropdown_DropdownValueChanged                    = OnDropdown_DropdownValueChanged;
		ui.onScrollRect_TemplateValueChanged                  = OnScrollRect_TemplateValueChanged;
		ui.onToggle_ItemValueChanged                          = OnToggle_ItemValueChanged;
		ui.onScrollbar_Scrollbar1ValueChanged                 = OnScrollbar_Scrollbar1ValueChanged;
		ui.onScrollRect_Scroll_ViewValueChanged               = OnScrollRect_Scroll_ViewValueChanged;
		ui.onScrollbar_Scrollbar_HorizontalValueChanged       = OnScrollbar_Scrollbar_HorizontalValueChanged;
		ui.onScrollbar_Scrollbar_VerticalValueChanged         = OnScrollbar_Scrollbar_VerticalValueChanged;
	}
	partial void OnAwake();
	partial void onInputField_InputFieldEndEdit(string value);
	partial void onButton_ButtonClick();
	partial void onToggle_ToggleValueChanged(bool value);
	partial void onSlider_SliderValueChanged(float value);
	partial void onScrollbar_ScrollbarValueChanged(float value);
	partial void onDropdown_DropdownValueChanged(int value);
	partial void onScrollRect_TemplateValueChanged(Vector2 value);
	partial void onToggle_ItemValueChanged(bool value);
	partial void onScrollbar_Scrollbar1ValueChanged(float value);
	partial void onScrollRect_Scroll_ViewValueChanged(Vector2 value);
	partial void onScrollbar_Scrollbar_HorizontalValueChanged(float value);
	partial void onScrollbar_Scrollbar_VerticalValueChanged(float value);

	private void OnInputField_InputFieldEndEdit(string value)
	{
		onInputField_InputFieldEndEdit(value);
	}
	private void OnButton_ButtonClick()
	{
		onButton_ButtonClick();
	}
	private void OnToggle_ToggleValueChanged(bool value)
	{
		onToggle_ToggleValueChanged(value);
	}
	private void OnSlider_SliderValueChanged(float value)
	{
		onSlider_SliderValueChanged(value);
	}
	private void OnScrollbar_ScrollbarValueChanged(float value)
	{
		onScrollbar_ScrollbarValueChanged(value);
	}
	private void OnDropdown_DropdownValueChanged(int value)
	{
		onDropdown_DropdownValueChanged(value);
	}
	private void OnScrollRect_TemplateValueChanged(Vector2 value)
	{
		onScrollRect_TemplateValueChanged(value);
	}
	private void OnToggle_ItemValueChanged(bool value)
	{
		onToggle_ItemValueChanged(value);
	}
	private void OnScrollbar_Scrollbar1ValueChanged(float value)
	{
		onScrollbar_Scrollbar1ValueChanged(value);
	}
	private void OnScrollRect_Scroll_ViewValueChanged(Vector2 value)
	{
		onScrollRect_Scroll_ViewValueChanged(value);
	}
	private void OnScrollbar_Scrollbar_HorizontalValueChanged(float value)
	{
		onScrollbar_Scrollbar_HorizontalValueChanged(value);
	}
	private void OnScrollbar_Scrollbar_VerticalValueChanged(float value)
	{
		onScrollbar_Scrollbar_VerticalValueChanged(value);
	}

}