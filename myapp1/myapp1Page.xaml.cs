using Xamarin.Forms;
using System;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Plugin.FilePicker;
using System.Threading;
using System.Threading.Tasks;
using PCLStorage;

namespace myapp1
{
	public partial class myapp1Page : ContentPage
	{
		string translatedNumber;
		static IAmazonS3 client;

		public myapp1Page()
		{
			InitializeComponent();
		}

		void OnTranslate(object sender, EventArgs e)
		{
			translatedNumber = Core.PhonewordTranslator.ToNumber(phoneNumberText.Text);
			if (!string.IsNullOrWhiteSpace(translatedNumber))
			{
				callButton.IsEnabled = true;
				callButton.Text = "Call " + translatedNumber;
			}
			else {
				callButton.IsEnabled = false;
				callButton.Text = "Call";
			}
		}

		async void OnCall(object sender, EventArgs e)
		{
			//var a = new CancellationTokenSource();
			Plugin.FilePicker.Abstractions.FileData fData = await CrossFilePicker.Current.PickFile();
			if (fData != null)
			{
				var myf = DependencyService.Get<IMyFile>();
				if (myf != null)
				{
					IFile f = myf.GetFile(fData.FilePath.ToString());

					//await DisplayAlert("Okay", fData.FilePath.ToString(), "Close");
					if (f != null)
					{
						using (client = new AmazonS3Client("", "", Amazon.RegionEndpoint.EUWest2))
						{
							PutObjectData(client, fData.FilePath.ToString());
						}
						/*byte[] buffer = new byte[100];
						using (System.IO.Stream stream = await f.OpenAsync(FileAccess.Read))
						{
						}*/
					}


				}
			/*if (await this.DisplayAlert(
"Dial a Number",
"Would you like to call " + translatedNumber + "?",
"Yes",
"No"))
{
var dialer = DependencyService.Get<IDialer>();
if (dialer != null)
dialer.Dial(translatedNumber);
}*/
			}
		}

		static void PutObjectData(IAmazonS3 client, string file)
		{
			TransferUtility fileTransferUtility =
	 new TransferUtility(client);
			TransferUtilityUploadRequest uploadRequest =
				new TransferUtilityUploadRequest
				{
					BucketName = "something2409840918",
					Key = "newone",
					FilePath = file
				};

			/*uploadRequest.UploadProgressEvent +=
				new EventHandler<UploadProgressArgs>
					(uploadRequest_UploadPartProgressEvent);*/


			fileTransferUtility.UploadAsync(uploadRequest);
		}
	}
}
