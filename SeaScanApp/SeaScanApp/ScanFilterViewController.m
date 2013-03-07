//
//  DateListView.m
//  SharkScan
//
//  Created by James Masterman on 9/11/12.
//
//

#import "ScanFilterViewController.h"
#import "SSDataManager.h"

#define BUTTON_FACE_DATE_STRING @"EEE d-MMM-yyyy"

@implementation ScanFilterViewController

@synthesize datePicker, dismissBlock, locationPicker;

- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        
        UIBarButtonItem* doneItem = [[UIBarButtonItem alloc]initWithBarButtonSystemItem:UIBarButtonSystemItemDone target:self action:@selector(save:)];
        
        [[self navigationItem] setRightBarButtonItem:doneItem];
        
        UIBarButtonItem* cancelItem = [[UIBarButtonItem alloc]initWithBarButtonSystemItem:UIBarButtonSystemItemCancel target:self action:@selector(cancel:)];
        
        [[self navigationItem] setLeftBarButtonItem:cancelItem];

    }
    return self;
}

- (void)viewDidLoad
{
    [super viewDidLoad];
	    
    // Initialization code
    datePicker.datePickerMode = UIDatePickerModeDate;
    datePicker.hidden = YES;
    datePicker.date = [NSDate date];
    [datePicker addTarget:self action:@selector(changeDateInLabel:) forControlEvents:UIControlEventValueChanged];
    
    locationPicker.hidden = YES;
    
    
    NSString* label = @" ";
    NSString* dateString = [SSSettings convertDateToString:[SSSettings earliestDate] formatString:BUTTON_FACE_DATE_STRING];
    [dateButton setTitle:[label stringByAppendingString:dateString] forState: UIControlStateNormal];

    
    label = @" ";
    NSString* locationString = @"All";
    SSLocation* selLoc = [[SSDataManager getInstance]getLocationByID:[SSSettings selectedLocationID]];
    if(selLoc != nil)
    {
        locationString = selLoc.location;
    }
    
    [locationButton setTitle:[label stringByAppendingString:locationString] forState:UIControlStateNormal];
    
}


- (void)viewDidUnload {
    locationPicker = nil;
    datePicker = nil;
    [super viewDidUnload];
}



-(void) changeDateInLabel:(id)sender
{
    NSLog(@"Changed Date Label");
    [SSSettings setEarliestDate:[datePicker date]];
    
    NSString* label = @" ";
    NSString* dateString = [SSSettings convertDateToString:[SSSettings earliestDate] formatString:BUTTON_FACE_DATE_STRING];
    [dateButton setTitle:[label stringByAppendingString:dateString]forState: UIControlStateNormal];
    
}

- (NSInteger)numberOfComponentsInPickerView:(UIPickerView *)thePickerView {
    
    return 1;
}

- (NSInteger)pickerView:(UIPickerView *)thePickerView numberOfRowsInComponent:(NSInteger)component {
    
    NSInteger count = 0;
    if([[SSDataManager getInstance]locations] != nil)
    {
        count = [[[SSDataManager getInstance]locations] count];
    }
    
    return count;
}

- (NSString *)pickerView:(UIPickerView *)thePickerView titleForRow:(NSInteger)row forComponent:(NSInteger)component
{
    
    NSString* item = @"";
    NSMutableDictionary* locations = [[SSDataManager getInstance]locations];
    if(locations != nil)
    {
        NSArray* locs = [locations allValues];
        locs = [locs sortedArrayUsingSelector:@selector(compare:)];
        if([locs count] > 0)
        {
            SSLocation* loc = [locs objectAtIndex:row];
            if(loc != nil)
            {
                item = loc.location;
            }
        }
    }
    
    return item;
}

- (void)pickerView:(UIPickerView *)thePickerView didSelectRow:(NSInteger)row inComponent:(NSInteger)component{
    
    NSMutableDictionary* locations = [[SSDataManager getInstance]locations];
    SSLocation* selectedLocation = nil;
    if(locations != nil)
    {
        NSArray* locs = [locations allValues];
        locs = [locs sortedArrayUsingSelector:@selector(compare:)];

        if([locs count] > 0)
        {
            selectedLocation = [locs objectAtIndex:row];
            NSLog(@"Selected loc: %@. Index of selected loc: %i", [selectedLocation location], row);

            [SSSettings setSelectedLocationID:[selectedLocation locID]];
    
            NSLog(@"Selected loc index:%d",[SSSettings selectedLocationID]);
    
            NSString* label = @" ";
            [locationButton setTitle:[label stringByAppendingString:[selectedLocation location]] forState:UIControlStateNormal];
        }
    }

    [locationPicker setHidden:YES];
}


-(void)save:(id)sender
{
    NSLog(@"saving");
    [[self presentingViewController] dismissViewControllerAnimated:YES completion:dismissBlock];
}

-(void)cancel:(id)sender
{
    NSLog(@"cancelling");
    [[self presentingViewController] dismissViewControllerAnimated:YES completion:nil];
}


- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (IBAction)selectLocation:(id)sender
{
    datePicker.hidden = YES;
    locationPicker.hidden = NO;
    
}
- (IBAction)selectDate:(id)sender;
{
    datePicker.hidden = NO;
    locationPicker.hidden = YES;
}

- (IBAction)selectOnlySuccesfullScans:(id)sender
{
    [SSSettings setOnlyShowMissionsWithTarget:[successfullScansSwitch isOn]];
}

- (IBAction)showTracklines:(id)sender
{
    [SSSettings setShowTracklines:[tracklineSwitch isOn]];
}

@end
