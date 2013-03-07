//
//  DateListView.h
//  SharkScan
//
//  Created by James Masterman on 9/11/12.
//
//

#import <UIKit/UIKit.h>

@interface ScanFilterViewController : UIViewController <UIPickerViewDataSource, UIPickerViewDelegate>

{
    IBOutlet UIDatePicker* datePicker;
    IBOutlet UIPickerView* locationPicker;
    IBOutlet UIButton* locationButton;
    IBOutlet UIButton* dateButton;
    IBOutlet UISwitch* successfullScansSwitch;
    IBOutlet UISwitch* tracklineSwitch;
    
}

@property (nonatomic, strong) UIDatePicker *datePicker;
@property (nonatomic, strong) UIPickerView *locationPicker;

@property (nonatomic, copy) void (^dismissBlock)(void);

- (IBAction)selectLocation:(id)sender;
- (IBAction)selectDate:(id)sender;
- (IBAction)selectOnlySuccesfullScans:(id)sender;
- (IBAction)showTracklines:(id)sender;

@end
